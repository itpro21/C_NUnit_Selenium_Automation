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
        private RegisterPage registerPage;

        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.GetDriver();
            driver.Navigate().GoToUrl(ConfigReader.Get("BaseUrl"));
            homePage = new HomePage(driver);
            registerPage = new RegisterPage(driver);
        }

        [TearDown]
        public void TearDown()
        {
            DriverFactory.QuitDriver();
        }

        [Test, TestCaseSource(typeof(TestDataReader), nameof(TestDataReader.GetRegisterUsers))]
        public void Verify_MandatoryFieldErrors_DisplayAndClear(string username, string email, string password, string confirmPassword)
        {
            TestContext.WriteLine("LOG001 - Mandatory field error messages display and clear");
            Navigate_to_Register_Page();
            VerifyMandatoryFieldErrors();
            FillFormData(username, email, password, confirmPassword);
            VerifyErrorsAreCleared();
        }

        private void Navigate_to_Register_Page()
        {
            TestContext.WriteLine("***Navigate to Home Page and click User Icon.***");
            homePage.ClickUserIcon();
            TestContext.WriteLine("***Click Create New Account to open Register Page.***");
            registerPage = homePage.ClickCreateNewAccount();
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
            registerPage.FillForm(username, email, password, confirmPassword);
        }

        private void VerifyErrorsAreCleared()
        {
            Assert.That(registerPage.AreAllErrorsCleared(), Is.True);
        }

    }
}
