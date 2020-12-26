using AutomatedUiTests.PageObjects;
using AutomatedUiTests.Shared;
using AutomatedUiTests.Shared.Contracts;
using AutomatedUiTests.Shared.Models;
using NUnit.Framework;

namespace AutomatedUiTests.TestCases
{
    [TestFixture(BrowserType.Chrome)]
    public class SampleTests : WebDriverFactory, ITests
    {
        public SampleTests(BrowserType browser)
            : base(browser)
        {
        }

        [OneTimeSetUp]
        public void Init()
        {
            Driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void ClearOnce()
        {
            DisposeWebDriver();
        }

        [TearDown]
        public void Clear()
        {
            TakeScreenshot($"{TestContext.CurrentContext.Test.Name}.png");
        }

        [Test]
        public void TestSomeFunctionality()
        {
            var loginPage = new MicrosoftLoginPage(Driver);
            loginPage.Login();
        }
    }
}