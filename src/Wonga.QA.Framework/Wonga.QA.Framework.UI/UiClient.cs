﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using Gallio.Framework;
using Gallio.Framework.Assertions;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.UiElements.Pages;
using Wonga.QA.Framework.UI.UiElements.Pages.Admin;
using Wonga.QA.Framework.UI.UiElements.Pages.Common;
using Wonga.QA.Framework.UI.UiElements.Pages.SalesForce;

namespace Wonga.QA.Framework.UI
{
    public class UiClient : IDisposable
    {
        public IWebDriver Driver;

        [Obsolete]
        public UiClient(String profileDir)
        {
            var firefoxProfile = new FirefoxProfile(profileDir);
            Driver = new FirefoxDriver(firefoxProfile);
        }

        public UiClient()
        {
            var capabillities = GetDesiredCapabilities();
            Driver = GetWebDriver(capabillities);
            if (Driver is CustomRemoteWebDriver)
                TestContext.CurrentContext.AddMetadata("JobURL", string.Format("https://saucelabs.com/jobs/{0}", ((CustomRemoteWebDriver)Driver).GetSessionId()));
        }

        private IWebDriver GetWebDriver(DesiredCapabilities capabilities)
        {
            if(Config.Ui.RemoteMode)
                return new CustomRemoteWebDriver(Config.Ui.RemoteUri, capabilities);
            switch (Config.Ui.Browser)
            {
                case(Config.UiConfig.BrowserType.Chrome):
                    return new ChromeDriver();
                case(Config.UiConfig.BrowserType.Firefox):
                    return new FirefoxDriver();
                case(Config.UiConfig.BrowserType.InternetExplorer):
                    var ieOps = new InternetExplorerOptions();
                    ieOps.IntroduceInstabilityByIgnoringProtectedModeSettings = true;
                    return new InternetExplorerDriver(ieOps);
                case(Config.UiConfig.BrowserType.Opera):
                    throw new NotImplementedException("Opera is not supported yet");
                case(Config.UiConfig.BrowserType.Safari):
                    throw new NotImplementedException("Safari is not supported by WebDriver");
                default:
                    throw new ArgumentException("Please select a Browser Type via the QAFBrowser environment variable");    
            }
        }

        private DesiredCapabilities GetDesiredCapabilities()
        {
            DesiredCapabilities capabilities;
            switch (Config.Ui.Browser)
            {
                case(Config.UiConfig.BrowserType.Chrome):
                    capabilities = DesiredCapabilities.Chrome();
                    break;
                case(Config.UiConfig.BrowserType.Firefox):
                    capabilities = DesiredCapabilities.Firefox();
                    break;
                case(Config.UiConfig.BrowserType.InternetExplorer):
                    capabilities = DesiredCapabilities.InternetExplorer();
                    break;
                case(Config.UiConfig.BrowserType.Opera):
                    capabilities = DesiredCapabilities.Opera();
                    break;
                case(Config.UiConfig.BrowserType.Safari):
                    throw new NotImplementedException("Safari is not supported yet");
                default:
                    throw new ArgumentException("Please select a QAFBrowser environment variable.");
            }
            capabilities.SetCapability(CapabilityType.Version, Config.Ui.BrowserVersion);
            capabilities.SetCapability(CapabilityType.Platform, new Platform(PlatformType.XP));
            capabilities.SetCapability("name", TestContext.CurrentContext.Test.Name);
            capabilities.SetCapability("username", Config.Ui.RemoteUsername);
            capabilities.SetCapability("accessKey", Config.Ui.RemoteApiKey);
            var tags = new List<String> { Config.SUT.ToString(), Config.AUT.ToString() };
            capabilities.SetCapability("tags", tags);
            capabilities.SetCapability("public", true);
            capabilities.SetCapability("idle-timeout", 60);
            capabilities.SetCapability("sauce-advisor", false);
            capabilities.SetCapability("record-screenshots", false);
            return capabilities;
        }

        public HomePage Home()
        {
            Driver.Navigate().GoToUrl(Config.Ui.Home);
            return new HomePage(this);
        }

        public LoginPage Login()
        {
            Driver.Navigate().GoToUrl(Config.Ui.Home + "/login");
            return new LoginPage(this);
        }

        public MyPaymentsPage Payments()
        {
            Driver.Navigate().GoToUrl(Config.Ui.Home + "/my-account/details");
            return new MyPaymentsPage(this);
        }
        
        public AboutUsPage About()
        {
            switch(Config.AUT)
            {
                case AUT.Za:
                case AUT.Ca:
                    Driver.Navigate().GoToUrl(Config.Ui.Home + "/about");
                    break;
                case AUT.Wb:
                    Driver.Navigate().GoToUrl(Config.Ui.Home + "/about-us");
                    break;
            }
            
            return new AboutUsPage(this);
        }

        public HowItWorksPage HowItWorks()
        {
            Driver.Navigate().GoToUrl(Config.Ui.Home + "/how-it-works");
            return new HowItWorksPage(this);
        }

        public PaymentCardsPage PaymentCards()
        {
            Driver.Navigate().GoToUrl(Config.Admin.Home + "/PaymentCards/GetList/00000000-0000-0000-0000-000000000000");
            return new PaymentCardsPage(this);
        }

        public AccountingPage Accounting()
        {
            Driver.Navigate().GoToUrl(Config.Admin.Home + "/Accounting");
            return new AccountingPage(this);
        }

        public Image Screen()
        {
            if (!(Driver is ITakesScreenshot))
                return null;
            var screenshot = ((ITakesScreenshot)Driver).GetScreenshot();
            var stream = new MemoryStream(screenshot.AsByteArray);
            return Image.FromStream(stream);
        }

        public String Source()
        {
            return Driver.PageSource;
        }

        public void Dispose()
        {
            Do.Until(() => { Driver.Quit(); return true; });
        }

        public SalesForceLoginPage SalesForceStart()
        {
            Driver.Navigate().GoToUrl(Config.SalesforceUi.Home);
            return new SalesForceLoginPage(this);
        }
    }
}
