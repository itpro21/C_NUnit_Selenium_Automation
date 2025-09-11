using OpenQA.Selenium;
using AdvantageShoppingTests.Utils;
using System;

namespace AdvantageShoppingTests.Pages
{
    public class RegisterPage
    {
        private readonly IWebDriver driver;
        private readonly WaitHelper _wait;

        private By usernameField = By.Name("usernameRegisterPage");
        private By emailField = By.Name("emailRegisterPage");
        private By passwordField = By.Name("passwordRegisterPage");
        private By confirmPasswordField = By.Name("confirm_passwordRegisterPage");
        private By form => By.Id("form");
        private By errorField = By.XPath("//label[@class='animated invalid']");
        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            _wait = new WaitHelper(driver);
        }
        public void EnterUsername(string username) => driver.FindElement(usernameField).SendKeys(username);
        public void EnterEmail(string email) => driver.FindElement(emailField).SendKeys(email);
        public void EnterPassword(string password) => driver.FindElement(passwordField).SendKeys(password);
        public void EnterConfirmPassword(string confirmPassword) => driver.FindElement(confirmPasswordField).SendKeys(confirmPassword);

        public void FillForm(string username, string email, string password, string confirmPassword)
        {
            EnterUsername(username);
            EnterEmail(email);
            EnterPassword(password);
            EnterConfirmPassword(confirmPassword);
        }

        public void FocusFields()
        {
            _wait.WaitForElementClickable(usernameField).Click();
            _wait.WaitForElementClickable(emailField).Click();
            _wait.WaitForElementClickable(passwordField).Click();
            _wait.WaitForElementClickable(confirmPasswordField).Click();
            _wait.WaitForElementClickable(form).Click();
        }

        public string GetUsernameError() => GetMandatoryErrorText(usernameField);
        public string GetEmailError() => GetMandatoryErrorText(emailField);
        public string GetPasswordError() => GetMandatoryErrorText(passwordField);
        public string GetConfirmPasswordError() => GetMandatoryErrorText(confirmPasswordField);

        public bool AreAllErrorsCleared()
        {
            return _wait.WaitForElementNotVisible(errorField);
        }

        public bool IsErrorDisplayed()
        {
            return _wait.WaitForElementVisible(errorField) != null;
        }

        private string GetMandatoryErrorText(By locator)
        {
            try
            {
                return driver.FindElement(locator).FindElement(By.XPath("following-sibling::label")).Text.Trim();
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }
    }
}
