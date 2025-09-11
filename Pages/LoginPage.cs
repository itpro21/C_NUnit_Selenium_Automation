using OpenQA.Selenium;
using AdvantageShoppingTests.Utils;
using System;

namespace AdvantageShoppingTests.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        private readonly WaitHelper _wait;

        private By createNewAccountLink => By.LinkText("CREATE NEW ACCOUNT");
        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WaitHelper(_driver);
        }
        public RegisterPage ClickCreateNewAccount()
        {
            _wait.SafeClick(createNewAccountLink);
            return new RegisterPage(_driver);
        }


    }
}