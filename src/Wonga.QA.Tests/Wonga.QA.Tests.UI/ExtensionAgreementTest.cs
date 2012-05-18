﻿using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Gallio.Framework.Assertions;
using System.Threading;
using MbUnit.Framework;
using OpenQA.Selenium;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Db.Extensions;
using Wonga.QA.Framework.Db.Payments;
using Wonga.QA.Framework.Db.Risk;
using Wonga.QA.Framework.UI;
using Wonga.QA.Framework.UI.Elements;
using Wonga.QA.Framework.UI.UiElements.Pages.Common;
using Wonga.QA.Framework.UI.UiElements.Sections;
using Wonga.QA.Tests.Core;
using Wonga.QA.Framework.UI.Mappings;

using System.ComponentModel;

namespace Wonga.QA.Tests.Ui
{
    class ExtensionAgreementTest : UiTest
    {
        [Test, AUT(AUT.Uk), JIRA("UK-971"), Pending("Fails due to bug UK-2114")]
        public void ExtensionAgreementPageTest()
        {
            string email = Get.RandomEmail();

            var customer = CustomerBuilder.New().WithEmailAddress(email).Build();
            var extensionDays = 7;
            var sExtensionDays = extensionDays.ToString();
            var application = ApplicationBuilder.New(customer).WithLoanAmount(150).WithLoanTerm(extensionDays).Build();

            var loginPage = Client.Login();
            var myAccountPage = loginPage.LoginAs(email);

            var mySummaryPage = myAccountPage.Navigation.MySummaryButtonClick();

            mySummaryPage.ChangePromiseDateButtonClick();
            var requestPage = new ExtensionRequestPage(this.Client);
            requestPage.SetExtendDays(sExtensionDays);

            // Set expected values
            var expectedRepaymentDate = Convert.ToDateTime(requestPage.RepaymentDate.Replace("st", "").Replace("nd", "").Replace("rd","").Replace("th", "")).ToString("dd/MM/yyyy");
            var expectedExtendedLoanTerm = application.LoanTerm + extensionDays;
            var expectedTotalToRepay = requestPage.TotalToRepay;
            var expectedLoanAmount = application.LoanAmount;
            var expectedRepresentativeAPR = "3784%";
            
            requestPage.setSecurityCode("123");
            requestPage.SubmitButtonClick();

            var extensionProcessingPage = new ExtensionProcessingPage(this.Client);
            var agreementPage = extensionProcessingPage.WaitFor<ExtensionAgreementPage>() as ExtensionAgreementPage;

            agreementPage.ClickExtensionSecciLink();
        
            Assert.IsTrue(agreementPage.secci.Text.Contains(ContentMap.Get.ExtensionAgreementPage.CreditInformation));
            Assert.IsTrue(agreementPage.secciPrint.Text.Contains(ContentMap.Get.ExtensionAgreementPage.PrintThisPage));
            Assert.IsTrue(agreementPage.secciHeader.Text.Contains(ContentMap.Get.ExtensionAgreementPage.ReadThis));
          
            Assert.Contains(agreementPage.secci.Text, expectedExtendedLoanTerm.ToString("#"));
            Assert.Contains(agreementPage.secci.Text, expectedRepaymentDate);
            Assert.Contains(agreementPage.secci.Text, expectedLoanAmount.ToString("#"));
            Assert.Contains(agreementPage.secci.Text, expectedTotalToRepay);
            Assert.Contains(agreementPage.secci.Text, expectedRepresentativeAPR);
        }

        [Test, AUT(AUT.Uk), JIRA("UK-971"), Pending("UK-2121")]
        [Row (2, 100, 1, 7)]
        public void ExtensionAgreementPageNDaysAfterLoanTest(int loanTerm, int loanAmount, int daysAfterLoan, int daysToExtend)
        {
            ExtensionAgreementPageNDaysAfterLoan(loanTerm, loanAmount, daysAfterLoan, daysToExtend);
        }


        private void ExtensionAgreementPageNDaysAfterLoan(int loanTerm, int loanAmount, int daysAfterLoan, int daysToExtend)
        {
            string email = Get.RandomEmail();

            var customer = CustomerBuilder.New().WithEmailAddress(email).Build();
            var application = ApplicationBuilder.New(customer).WithLoanTerm(loanTerm).WithLoanAmount(loanAmount).Build();

            // Rewind application dates
            ApplicationEntity applicationEntity = Drive.Db.Payments.Applications.Single(a => a.ExternalId == application.Id);
            RiskApplicationEntity riskApplication = Drive.Db.Risk.RiskApplications.Single(r => r.ApplicationId == application.Id);
            Drive.Db.RewindApplicationDates(applicationEntity, riskApplication, TimeSpan.FromDays(daysAfterLoan));

            var loginPage = Client.Login();
            var myAccountPage = loginPage.LoginAs(email);

            var mySummaryPage = myAccountPage.Navigation.MySummaryButtonClick();

            mySummaryPage.ChangePromiseDateButtonClick();
            var requestPage = new ExtensionRequestPage(this.Client);
            requestPage.SetExtendDays(daysToExtend.ToString("#"));

            // Set expected values
            var expectedExtendedLoanTerm = application.LoanTerm - daysAfterLoan + daysToExtend;
            var expectedRepaymentDate = Convert.ToDateTime(requestPage.RepaymentDate.Replace("st", "").Replace("nd", "").Replace("rd", "").Replace("th", "")).ToString("dd/MM/yyyy");
            var expectedTotalToRepay = requestPage.TotalToRepay;
            var expectedLoanAmount = application.LoanAmount;
            var expectedRepresentativeAPR = "3784%";

            requestPage.setSecurityCode("123");
            requestPage.SubmitButtonClick();

            var extensionProcessingPage = new ExtensionProcessingPage(this.Client);
            var agreementPage = extensionProcessingPage.WaitFor<ExtensionAgreementPage>() as ExtensionAgreementPage;

            agreementPage.ClickExtensionSecciLink();

            Assert.Contains(agreementPage.secci.Text, expectedExtendedLoanTerm.ToString("#"));
            Assert.Contains(agreementPage.secci.Text, expectedRepaymentDate);
            Assert.Contains(agreementPage.secci.Text, expectedLoanAmount.ToString("#"));
            Assert.Contains(agreementPage.secci.Text, expectedTotalToRepay);
            Assert.Contains(agreementPage.secci.Text, expectedRepresentativeAPR);
        }
    }
}