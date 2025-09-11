using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace AdvantageShoppingTests.Utils
{
    public class WaitHelper
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        int timeoutInSeconds = int.Parse(ConfigReader.Get("ExplicitWait"));

        public WaitHelper(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public IWebElement WaitForElementVisible(By locator)
        {
            return _wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return element != null ? element : null;
            });
        }

        public IWebElement WaitForElementClickable(By locator)
        {
            return _wait.Until(driver =>
            {
                var element = driver.FindElement(locator);
                return (element != null && element.Displayed && element.Enabled) ? element : null;
            });
        }

        public bool WaitForElementNotVisible(By locator)
        {
            return _wait.Until(driver =>
            {
                try
                {
                    return driver.FindElement(locator).Displayed == false;
                }
                catch (NoSuchElementException)
                {
                    return true; // element not present = not visible
                }
            });
        }

        public void SafeClick(By locator)
        {
            var element = WaitForElementClickable(locator);
            try
            {
                element.Click();
            }
            catch (ElementClickInterceptedException)
            {
                ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].click();", element);
            }
        }
    }
}