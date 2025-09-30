using OpenQA.Selenium;
using NUnit.Framework;
using AdvantageShoppingTests.Driver;
using AdvantageShoppingTests.Pages;
using AdvantageShoppingTests.Utils;
using AventStack.ExtentReports;
using NUnit.Framework.Interfaces;

namespace AdvantageShoppingTests.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected HomePage homePage;
        protected RegisterPage registerPage;
        protected static ExtentReports extent;
        protected ExtentTest test;

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
    }
}