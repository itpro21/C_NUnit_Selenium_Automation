using OpenQA.Selenium;
using NUnit.Framework;
using AdvantageShoppingTests.Driver;
using AdvantageShoppingTests.Pages;
using AdvantageShoppingTests.Utils;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace AdvantageShoppingTests.Tests
{
    [TestFixture]
    public class CreateAccountTests
    {
        private IWebDriver driver;
        private HomePage homePage;
        private RegisterPage registerPage;

        private ExtentReports extent;
        private ExtentTest test;

        // One Time Setup for TestReports
        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            extent = TestReportManager.GetExtent();
        }

        // Test Setup and TearDown for each test
        [SetUp]
        public void SetUp()
        {
            driver = DriverFactory.GetDriver();
            driver.Navigate().GoToUrl(ConfigReader.Get("BaseUrl"));
            homePage = new HomePage(driver);
            registerPage = new RegisterPage(driver);
            test = extent.CreateTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void TearDown()
        {
            var outcome = TestContext.CurrentContext.Result.Outcome.Status;
            var stacktrace = TestContext.CurrentContext.Result.StackTrace;

            if (outcome == TestStatus.Failed)
            {
                test.Fail("Test Failed");
                if (!string.IsNullOrEmpty(stacktrace))
                {
                    test.Fail(stacktrace);
                }
            }
            else if (outcome == TestStatus.Passed)
            {
                test.Pass("Test Passed");
            }
            else
            {
                test.Skip("Test Skipped");
            }
            DriverFactory.QuitDriver();
        }
        // One Time TearDown for TestReports
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            extent.Flush();
        }

        [Test, TestCaseSource(typeof(TestDataReader), nameof(TestDataReader.GetRegisterUsers))]
        public void Verify_MandatoryFieldErrors_DisplayAndClear(string username, string email, string password, string confirmPassword)
        {
            test.Info($"Running the Test in browser : " + ConfigReader.Get("Browser"));
            Navigate_to_Register_Page();
            VerifyMandatoryFieldErrors();
            FillFormData(username, email, password, confirmPassword);
            VerifyErrorsAreCleared();
            test.Info($"LOG001 - Mandatory field error messages display and clear");
        }

        // Click UserIcon and Navigate to Register Page
        private void Navigate_to_Register_Page()
        {
            test.Info($"Step 1 - Navigate to Home Page and click User Icon");
            homePage.ClickUserIcon();
            test.Info($"Step 2 - Click Create New Account to open Register Page");
            registerPage = homePage.ClickCreateNewAccount();
        }

        // Verify Error message displayed for mandatory fields
        private void VerifyMandatoryFieldErrors()
        {
            test.Info($"Step 3 - Click into and out of the Username, Email, Password and Confirm Password fields to check mandatory field validation");
            registerPage.FocusFields();
            Assert.That(registerPage.GetUsernameError(), Is.EqualTo("Username field is required"));
            Assert.That(registerPage.GetEmailError(), Is.EqualTo("Email field is required"));
            Assert.That(registerPage.GetPasswordError(), Is.EqualTo("Password field is required"));
            Assert.That(registerPage.GetConfirmPasswordError(), Is.EqualTo("Confirm password field is required"));
        }

        // Fill in the form data
        private void FillFormData(string username, string email, string password, string confirmPassword)
        {
            test.Info($"Step 4 - Enter Form Data ");
            registerPage.FillForm(username, email, password, confirmPassword);
        }

        // Verify all error messages are cleared
        private void VerifyErrorsAreCleared()
        {
            Assert.That(registerPage.AreAllErrorsCleared(), Is.True);
        }
    }
}
