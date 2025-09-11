using NUnit.Framework;
using OpenQA.Selenium;
using AdvantageShoppingTests.Driver;
using AdvantageShoppingTests.Pages;
using AdvantageShoppingTests.Utils;

namespace AdvantageShoppingTests.Tests
{
    [TestFixture]
    public class CreateAccountTests
    {
        private IWebDriver driver;
        private HomePage homePage;
        private LoginPage loginPage;
        private RegisterPage registerPage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.GetDriver();
            driver.Navigate().GoToUrl(ConfigReader.Get("BaseUrl"));
            homePage = new HomePage(driver);
            loginPage = new LoginPage(driver);
            registerPage = new RegisterPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            DriverFactory.QuitDriver();
        }

        [TestCase("testuser2", "test2@email.com", "Password123", "Password123")]
        public void Verify_MandatoryFieldErrors_DisplayAndClear(string username, string email, string password, string confirmPassword)
        {
            TestContext.WriteLine("LOG001 - Mandatory field error messages display and clear");
            Navigate_to_Register_Page();
            VerifyMandatoryFieldErrors();
            FillFormData(username, email, password, confirmPassword);
            VerifyErrorsAreCleared();
        }
        [TestCase("testuser4", "test4?email.com", "Password3455678", "Password3456578")]
        public void Verify_ErrorDisplayed_for_InvalidData(string username, string email, string password, string confirmPassword)
        {
            TestContext.WriteLine("LOG002 - Error Message displayed for Invalid data");
            Navigate_to_Register_Page();
            FillFormData(username, email, password, confirmPassword);
            VerifyErrorDisplayed();
        }
        private void Navigate_to_Register_Page()
        {
            TestContext.WriteLine("***Navigate to Home Page and click User Icon.***");
            loginPage = homePage.ClickUserIcon();
            TestContext.WriteLine("***Click Create New Account to open Register Page.***");
            registerPage = loginPage.ClickCreateNewAccount();
        }

        private void VerifyMandatoryFieldErrors()
        {
            TestContext.WriteLine("***Click into and out of the Username, Email, Password and Confirm Password fields to check mandatory field validation.***");
            registerPage.FocusFields();
            Assert.That(registerPage.GetUsernameError(), Is.EqualTo("Username field is required"));
            Assert.That(registerPage.GetEmailError(), Is.EqualTo("Email field is required"));
            Assert.That(registerPage.GetPasswordError(), Is.EqualTo("Password field is required"));
            Assert.That(registerPage.GetConfirmPasswordError(), Is.EqualTo("Confirm password field is required"));
        }

        private void FillFormData(string username, string email, string password, string confirmPassword)
        {
            TestContext.WriteLine("***Enter Form Data***");
            registerPage.EnterUsername(username);
            registerPage.EnterEmail(email);
            registerPage.EnterPassword(password);
            registerPage.EnterConfirmPassword(confirmPassword);
        }

        private void VerifyErrorsAreCleared()
        {
            Assert.That(registerPage.AreAllErrorsCleared(), Is.True);
        }

        private void VerifyErrorDisplayed()
        {
            Assert.That(registerPage.IsErrorDisplayed(), Is.True);
        }
    }
}
