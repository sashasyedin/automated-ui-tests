using AutomatedUiTests.Shared.Models;
using Microsoft.Edge.SeleniumTools;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;

namespace AutomatedUiTests.Shared
{
    /// <summary>
    /// A static factory object for creating WebDriver instances
    /// </summary>
    public abstract class WebDriverFactory
    {
        protected WebDriverFactory(BrowserType type)
            : this()
        {
            Driver = CreateWebDriver(type);
        }

        private WebDriverFactory()
        {
            Config = Configuration.InitConfiguration();
        }

        protected IConfiguration Config { get; }
        protected IWebDriver Driver { get; }

        public static IWebDriver CreateWebDriver(BrowserType type)
        {
            var driver = null as IWebDriver;

            switch (type)
            {
                case BrowserType.Chrome:
                    driver = CreateChromeDriver();
                    break;
                case BrowserType.Firefox:
                    driver = CreateFirefoxDriver();
                    break;
                case BrowserType.Edge:
                    driver = CreateEdgeDriver();
                    break;
                case BrowserType.InternetExplorer:
                    driver = CreateIeDriver();
                    break;
            }

            return driver;
        }

        public void TakeScreenshot(string fileName)
        {
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var screenshotFile = Path.Combine(TestContext.CurrentContext.WorkDirectory, fileName);
            screenshot.SaveAsFile(screenshotFile, ScreenshotImageFormat.Png);
            TestContext.AddTestAttachment(screenshotFile, "Failure Screenshot");
        }

        /// <summary>
        /// Creates Chrome Driver instance.
        /// </summary>
        /// <returns>A new instance of Chrome Driver.</returns>
        private static IWebDriver CreateChromeDriver()
        {
            var options = new ChromeOptions();
            IWebDriver driver = new ChromeDriver(GetWebDriverPath(Constants.ChromeWebDriverEnvVar), options);
            return driver;
        }

        /// <summary>
        /// Creates Firefox Driver instance.
        /// </summary>
        /// <returns>A new instance of Firefox Driver.</returns>
        private static IWebDriver CreateFirefoxDriver()
        {
            var options = new FirefoxOptions();
            IWebDriver driver = new FirefoxDriver(GetWebDriverPath(Constants.FirefoxWebDriverEnvVar), options);
            return driver;
        }

        /// <summary>
        /// Creates Edge Driver instance.
        /// </summary>
        /// <returns>A new instance of Edge Driver.</returns>
        private static IWebDriver CreateEdgeDriver()
        {
            var options = new EdgeOptions { UseChromium = true };
            var service = EdgeDriverService.CreateChromiumService(GetWebDriverPath(Constants.EdgeWebDriverEnvVar), Constants.EdgeWebDriverExe);
            IWebDriver driver = new EdgeDriver(service, options);
            return driver;
        }

        /// <summary>
        /// Creates Internet Explorer Driver instance.
        /// </summary>
        /// <returns>A new instance of IE Driver.</returns>
        private static IWebDriver CreateIeDriver()
        {
            var options = new InternetExplorerOptions
            {
                UnhandledPromptBehavior = UnhandledPromptBehavior.Accept,
                EnableNativeEvents = false,
                EnablePersistentHover = true,
                IntroduceInstabilityByIgnoringProtectedModeSettings = true,
                IgnoreZoomLevel = true,
                EnsureCleanSession = true
            };

            IWebDriver driver = new InternetExplorerDriver(GetWebDriverPath(Constants.IeWebDriverEnvVar), options);
            return driver;
        }

        private static string GetWebDriverPath(string envVar)
        {
            var webDriverPath = Environment.GetEnvironmentVariable(envVar);

            return string.IsNullOrEmpty(webDriverPath)
                ? Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory)
                : webDriverPath;
        }

        protected void DisposeWebDriver()
        {
            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}