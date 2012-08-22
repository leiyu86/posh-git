﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Threading;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Msmq.Enums.Integration.Risk;
using Wonga.QA.Framework.UI;
using Wonga.QA.Framework.UI.UiElements.Pages.Common;
using Wonga.QA.Tests.Core;
using Wonga.QA.Framework.Api;

namespace Wonga.QA.UiTests.Web.Region.Uk.L0Ln
{
    [Parallelizable(TestScope.All), AUT(AUT.Uk)]
    class UkL0LnDeclineTests : UiTest
    {
        private Dictionary<string, string> _outputData = new Dictionary<string, string> { };


       // private static string _footerTextMessage = " Please find the contact details below to obtain a free copy of your credit report.\r\n " + "Experian Automated advice line: 0844 481 8000 consumer.helpservice@uk.experian.com" + "Customer Support Centre PO Box 1136 Warrington WA4 9GQ\r\n\r\nCallcredit " + "General questions: care@callcreditcheck.com Customer Care Callcredit check PO Box 734 Leeds LS1 9GX";
        public enum RiskMaskForDeclinedLoan
        {
            TESTBankAccountMatchedToApplicant,
            //TESTExperianCreditBureauDataIsAvailable,
            //TESTDateOfBirth, //-- does not work
            //TESTCustomerDateOfBirthIsCorrect, -- does not work
            //TESTPaymentCardIsValid, -- does not work
            //TESTCardMask, -- does not work
            //TESTIsAlive, -- does not work
            //TESTIsSolvent, -- does not work
            //TESTMonthlyIncome, -- does not work
            //TESTCreditBureauDataIsAvailable, -- works fine
            //TESTCustomerIsSolvent, -- does not work
            //TESTCustomerDateOfBirthIsCorrectSME, -- does not work
            //TESTMonthlyIncomeEnoughForRepayment, -- does not work
            //TESTRepaymentPredictionPositive, -- does not work
            //TESTCallValidatePaymentCardIsValid, -- does not work
            //TESTExperianPaymentCardIsValid, -- does not work
            //TESTRiskPaymentCardIsValid, -- does not work          
            //TESTExperianCustomerIsSolvent, -- does not work
            //TESTExperianCustomerDateOfBirthIsCorrect, -- does not work
            //TESTExperianCustomerDateOfBirthIsCorrectSME, -- does not work
        }

        [FixtureSetUp]
        public void MySetUp()
        {

        }

        [FixtureTearDown]
        private void MyTearDown()
        {
            // Output the emails of the created users
            Debug.WriteLine("**************");
            foreach (var outputData in _outputData)
            {
                Debug.WriteLine("{0},  {1}", outputData.Key, outputData.Value);
            }
            Debug.WriteLine("**************");
        }

        [Test, JIRA("UKWEB-253"), Owner(Owner.StanDesyatnikov, Owner.PavithranVangiti)]
        [Pending ("Test in development")]
        [Row(RiskMaskForDeclinedLoan.TESTBankAccountMatchedToApplicant)]
        //[Row(RiskMaskForDeclinedLoan.TESTDateOfBirth)]
        //[Row(RiskMaskForDeclinedLoan.TESTExperianCreditBureauDataIsAvailable)]
        public void L0DeclinedWithVariousAdvices(RiskMaskForDeclinedLoan riskMask)
        //public void L0DeclinedWithVariousAdvices([EnumData(typeof(RiskMaskForDeclinedLoan))]RiskMaskForDeclinedLoan riskMask)
        {
            var email = Get.RandomEmail();

            _outputData.Add(email, Get.EnumToString(riskMask));

            Client.Driver.Manage().Window.Maximize();
            var journeyL0 = JourneyFactory.GetL0Journey(Client.Home())
                .WithEmail(email)
                .WithEmployerName(Get.EnumToString(riskMask))
                .WithDeclineDecision();
                //.WithEmployerName(RiskMask.TESTBlacklist.ToString());
            var declinedPage = journeyL0.Teleport<DeclinedPage>() as DeclinedPage;

            Assert.IsTrue(declinedPage.DeclineAdviceExists());
            //Assert.AreEqual(DeclineAdvices[riskMask], declinedPage.DeclineAdvice());
            //Console.WriteLine("L0 Decline Advice: {0}", declinedPage.DeclineAdvice());

            // TODO: check that "here" link in Decline Advice leads to correct page. Now it leads to Wonga.com/my-account, which does not exists.

            // TODO: Ln fails with Nearly There page instead of Decline Page
            // log in
            //var loginPage = Client.Login();
            //var mySummaryPage = loginPage.LoginAs(email);

            //var journeyLn = JourneyFactory.GetLnJourney(Client.Home());
            //var processingPage = journeyLn.Teleport<ProcessingPage>() as ProcessingPage;

            //Assert.IsTrue(declinedPage.DeclineAdviceExists());
            //Console.WriteLine("Ln Decline Advice: {0}", declinedPage.DeclineAdvice());
        }
    }
}
