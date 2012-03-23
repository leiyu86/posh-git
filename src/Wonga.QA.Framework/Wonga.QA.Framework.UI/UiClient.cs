﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.UiElements.Pages;
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
            Driver = new FirefoxDriver();
            //Driver = new InternetExplorerDriver();
            //Driver = new ChromeDriver();
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
            Driver.Navigate().GoToUrl(Config.Ui.Home + "/about");
            return new AboutUsPage(this);
        }

        public Image Screen()
        {
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
