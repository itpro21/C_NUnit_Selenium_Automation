using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using System;
using AdvantageShoppingTests.Utils;

namespace AdvantageShoppingTests.Driver
{
    public static class DriverFactory
    {
        [ThreadStatic]
        private static IWebDriver driver;

        public static IWebDriver GetDriver()
        {
            if (driver == null)
            {
                string browser = ConfigReader.Get("Browser") ?? "chrome";
                bool headless = bool.Parse(ConfigReader.Get("Headless") ?? "false");

                if (browser.ToLower() == "chrome")
                {
                    var options = new ChromeOptions();
                    if (headless) options.AddArgument("--headless=new");
                    driver = new ChromeDriver(options);
                }
                else if (browser.ToLower() == "firefox")
                {
                    var options = new FirefoxOptions();
                    if (headless) options.AddArgument("--headless");
                    driver = new FirefoxDriver(options);
                }
                else
                {
                    throw new ArgumentException($"Unsupported browser: {browser}");
                }

                driver.Manage().Timeouts().ImplicitWait =
                    TimeSpan.FromSeconds(Convert.ToInt32(ConfigReader.Get("ImplicitWait") ?? "5"));
                driver.Manage().Window.Maximize();
            }
            return driver;
        }

        public static void QuitDriver()
        {
            driver?.Quit();
            driver = null;
        }
    }
}
