using OpenQA.Selenium;
using AdvantageShoppingTests.Utils;
using System;

namespace AdvantageShoppingTests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private readonly WaitHelper _wait;
        private By userIcon => By.Id("menuUser");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WaitHelper(_driver);
        }
        public LoginPage ClickUserIcon()
        {
            _wait.SafeClick(userIcon);
            return new LoginPage(_driver);
        }
    }
}