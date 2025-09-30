using OpenQA.Selenium;
using AdvantageShoppingTests.Utils;

namespace AdvantageShoppingTests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private readonly WaitHelper _wait;
        private static By UserIcon => By.Id("menuUser");
        private static By CreateNewAccountLink => By.LinkText("CREATE NEW ACCOUNT");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WaitHelper(_driver);
        }

        public void ClickUserIcon()
        {
            _wait.SafeClick(UserIcon);
        }

        public RegisterPage ClickCreateNewAccount()
        {
            _wait.SafeClick(CreateNewAccountLink);
            return new RegisterPage(_driver);
        }
    }
}