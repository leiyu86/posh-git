﻿using MbUnit.Framework;
using OpenQA.Selenium;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Tests.Core;


namespace Wonga.QA.UiTests.Web.Region.Ca.MyAccounts
{
    [Parallelizable(TestScope.All), AUT(AUT.Ca)]
    class MySummary : UiTest
    {

        [Test]
        public void IsRepaymentWarningAvailableForLn()
        {
            var loginPage = Client.Login();
            string email = Get.RandomEmail();
            CustomerBuilder.New().WithEmailAddress(email).Build();
            var summary = loginPage.LoginAs(email);

            summary.Client.Driver.Navigate().GoToUrl(Config.Ui.Home + "/repay-canada");
            var xpath = summary.Client.Driver.FindElement(By.CssSelector("#content-area p:nth-child(1)"));
            Assert.IsTrue(xpath.Text.Contains("via online banking"));
        }

        [Test]
        public void ThisIsTestSoBuggerOff()
        {

        }
    }
}
