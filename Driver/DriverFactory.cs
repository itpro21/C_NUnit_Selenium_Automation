using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using AdvantageShoppingTests.Utils;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace AdvantageShoppingTests.Driver
{
    public static class DriverFactory
    {
        [ThreadStatic]
        private static IWebDriver driver;

        public static IWebDriver GetDriver()
        {
            string gridUrl = ConfigReader.Get("GRID_URL") ?? "http://localhost:4444/wd/hub";
            string browserConfig = ConfigReader.Get("Browser") ?? "chrome";
            bool headless = bool.Parse(ConfigReader.Get("Headless") ?? "false");
            string browser = browserConfig.ToLower();
            if (browser == "chrome")
            {
                var options = new ChromeOptions();
                if (headless) options.AddArgument("--headless=new");
                driver = new ChromeDriver(options);
            }
            else if (browser == "firefox")
            {
                var options = new FirefoxOptions();
                if (headless) options.AddArgument("--headless");
                driver = new FirefoxDriver(options);
            }
            else if (browser == "edge")
            {
                var options = new EdgeOptions();
                if (headless) options.AddArgument("--headless");
                driver = new EdgeDriver();
            }
            else if (browser == "remotechrome")
            {
                driver = new RemoteWebDriver(new Uri(gridUrl), new ChromeOptions());
            }
            else if (browser == "remotefirefox")
            {
                driver = new RemoteWebDriver(new Uri(gridUrl), new FirefoxOptions());
            }
            else if (browser == "remoteedge")
            {
                driver = new RemoteWebDriver(new Uri(gridUrl), new EdgeOptions());
            }
            else
            {
                throw new ArgumentException($"Unsupported browser: {browser}");
            }

            driver.Manage().Timeouts().ImplicitWait =
                    TimeSpan.FromSeconds(Convert.ToInt32(ConfigReader.Get("ImplicitWait") ?? "5"));
            driver.Manage().Window.Maximize();

            return driver;
        }

        public static void QuitDriver()
        {
            driver?.Quit();
            driver = null;
        }
    }
}
