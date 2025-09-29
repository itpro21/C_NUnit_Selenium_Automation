using OpenQA.Selenium;
using AdvantageShoppingTests.Utils;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Support.UI;
using System;

namespace AdvantageShoppingTests.Pages
{
    public class RegisterPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;
        public int TimeoutInSeconds = int.Parse(ConfigReader.Get("ExplicitWait"));

        private readonly By usernameField = By.Name("usernameRegisterPage");
        private readonly By emailField = By.Name("emailRegisterPage");
        private readonly By passwordField = By.Name("passwordRegisterPage");
        private readonly By confirmPasswordField = By.Name("confirm_passwordRegisterPage");
        private static By Form => By.Id("form");
        private readonly By errorField = By.XPath("//label[@class='animated invalid']");
        public RegisterPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(TimeoutInSeconds));
        }

        public void FillForm(string username, string email, string password, string confirmPassword)
        {
            _driver.FindElement(usernameField).SendKeys(username);
            _driver.FindElement(emailField).SendKeys(email);
            _driver.FindElement(passwordField).SendKeys(password);
            _driver.FindElement(confirmPasswordField).SendKeys(confirmPassword);
        }

        public void FocusFields()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(usernameField)).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(emailField)).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(passwordField)).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(confirmPasswordField)).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(Form)).Click();
        }

        public string GetUsernameError() => GetMandatoryErrorText(usernameField);
        public string GetEmailError() => GetMandatoryErrorText(emailField);
        public string GetPasswordError() => GetMandatoryErrorText(passwordField);
        public string GetConfirmPasswordError() => GetMandatoryErrorText(confirmPasswordField);

        public bool AreAllErrorsCleared()
        {
            return _wait.Until(ExpectedConditions.InvisibilityOfElementLocated(errorField));
        }

        private string GetMandatoryErrorText(By locator)
        {
            try
            {
                return _driver.FindElement(locator).FindElement(By.XPath("following-sibling::label")).Text.Trim();
            }
            catch (NoSuchElementException)
            {
                return string.Empty;
            }
        }
    }
}
