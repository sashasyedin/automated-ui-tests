using AutomatedUiTests.Shared;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;

namespace AutomatedUiTests.PageObjects
{
    public class MicrosoftLoginPage
    {
        private IConfiguration _config;
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public MicrosoftLoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            _config = Configuration.InitConfiguration();
            PageFactory.InitElements(driver, this);
        }

        public void Login()
        {
            _driver.Navigate().GoToUrl("https://login.microsoftonline.com");

            var email = _config["TestUserEmail"];
            var password = _config["TestUserPassword"];

            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("i0116"))).SendKeys(email);
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("idSIButton9"))).Click();
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("i0118"))).SendKeys(password);
            _wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("idSIButton9"))).Click();
        }
    }
}