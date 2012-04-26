﻿using System;
using System.Threading;
using MbUnit.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI;
using Wonga.QA.Framework.UI.UiElements.Pages.Common;
using Wonga.QA.Framework.UI.UiElements.Pages.Wb;
using Wonga.QA.Tests.Core;
using Wonga.QA.Framework.Api;

namespace Wonga.QA.Tests.Ui
{
    public class L0AcceptedLoan : UiTest
    {
        private const String MiddleNameMask = "TESTNoCheck";

        [Test, AUT(AUT.Za)]
        public void ZaAcceptedLoan()
        {
            var journey = JourneyFactory.GetL0Journey(Client.Home()); 
            var processingPage = journey.ApplyForLoan(200, 10)
                                 .FillPersonalDetails(Get.EnumToString(RiskMask.TESTEmployedMask))
                                 .FillAddressDetails()
                                 .FillAccountDetails()
                                 .FillBankDetails()
                                 .CurrentPage as ProcessingPage;

            var acceptedPage = processingPage.WaitFor<AcceptedPage>() as AcceptedPage;
            acceptedPage.SignAgreementConfirm();
            acceptedPage.SignDirectDebitConfirm();
            var dealDone = acceptedPage.Submit();
        }

       [Test, AUT(AUT.Ca), Pending("CA WIP,RC FE seems broken - postponing the push of the selenium tests")]
        public void CaAcceptedLoan()
        {
            var journey = JourneyFactory.GetL0Journey(Client.Home()); 
            var processingPage = journey.ApplyForLoan(200, 10)
                                 .FillPersonalDetails(Get.EnumToString(RiskMask.TESTEmployedMask))
                                 .FillAddressDetails()
                                 .FillAccountDetails()
                                 .FillBankDetails()
                                 .CurrentPage as ProcessingPage;

            var acceptedPage = processingPage.WaitFor<AcceptedPage>() as AcceptedPage;
            acceptedPage.SignConfirmCaL0(DateTime.Now.ToString("d MMM yyyy"), _firstName, _lastName);
            var dealDone = acceptedPage.Submit();
        }

       [Test, AUT(AUT.Wb)]
       public void WbAcceptedLoan()
       {
           var journey = JourneyFactory.GetL0JourneyWB(Client.Home());
           var homePage = journey.ApplyForLoan(5500, 30)
               .AnswerEligibilityQuestions()
               .FillPersonalDetails(MiddleNameMask)
               .FillAddressDetails("More than 4 years")
               .FillAccountDetails()
               .FillBankDetails()
               .FillCardDetails()
               .EnterBusinessDetails()
               .DeclineAddAdditionalDirector()
               .EnterBusinessBankAccountDetails()
               .EnterBusinessDebitCardDetails()
               .WaitForApplyTermsPage()
               .ApplyTerms()
               .FillAcceptedPage()
               .GoHomePage();
       }

       [Test, AUT(AUT.Wb)]
       public void WbAcceptedLoanAddAdditionalDirector()
       {
           var journey = JourneyFactory.GetL0JourneyWB(Client.Home());
           var homePage = journey.ApplyForLoan(5500, 30)
               .AnswerEligibilityQuestions()
               .FillPersonalDetails(MiddleNameMask)
               .FillAddressDetails("2 to 3 years")
               .FillAccountDetails()
               .FillBankDetails()
               .FillCardDetails()
               .EnterBusinessDetails()
               .AddAdditionalDirector()
               .EnterBusinessBankAccountDetails()
               .EnterBusinessDebitCardDetails()
               .WaitForApplyTermsPage()
               .ApplyTerms()
               .FillAcceptedPage()
               .GoHomePage();
       }

       [Test, AUT(AUT.Wb)]
       public void WbAcceptedLoanUpdateLoanDurationOnApplyTermsPage()
       {
           var journey = JourneyFactory.GetL0JourneyWB(Client.Home());
           var homePage = journey.ApplyForLoan(5500, 30)
               .AnswerEligibilityQuestions()
               .FillPersonalDetails(MiddleNameMask)
               .FillAddressDetails("3 to 4 years")
               .FillAccountDetails()
               .FillBankDetails()
               .FillCardDetails()
               .EnterBusinessDetails()
               .DeclineAddAdditionalDirector()
               .EnterBusinessBankAccountDetails()
               .EnterBusinessDebitCardDetails()
               .WaitForApplyTermsPage()
               .UpdateLoanDuration()
               .ApplyTerms()
               .FillAcceptedPage()
               .GoHomePage();
       }     

       [Test, AUT(AUT.Wb)]
       public void WbAcceptedLoanAddressLessThan2Years()
       {
           var journey = JourneyFactory.GetL0JourneyWB(Client.Home());
           var homePage = journey.ApplyForLoan(5500, 30)
               .AnswerEligibilityQuestions()
               .FillPersonalDetails(MiddleNameMask)
               .FillAddressDetails("Between 4 months and 2 years")
               .EnterAdditionalAddressDetails()
               .FillAccountDetails()
               .FillBankDetails()
               .FillCardDetails()
               .EnterBusinessDetails()
               .DeclineAddAdditionalDirector()
               .EnterBusinessBankAccountDetails()
               .EnterBusinessDebitCardDetails()
               .WaitForApplyTermsPage()
               .ApplyTerms()
               .FillAcceptedPage()
               .GoHomePage();

       }

        [Test, AUT(AUT.Uk)]
        public void UkAcceptedLoan()
        {
            var journey = JourneyFactory.GetL0Journey(Client.Home());

            var acceptedPage = journey.ApplyForLoan(200, 10)
                                     .FillPersonalDetails(Get.EnumToString(RiskMask.TESTEmployedMask))
                                     .FillAddressDetails()
                                     .FillAccountDetails()
                                     .FillBankDetails()
                                     .FillCardDetails()
                                     .WaitForAcceptedPage().CurrentPage as AcceptedPage;

        }

        [Test, AUT(AUT.Uk), JIRA("UK-730")]
        public void CheckLoanAgreement()
        {
            var journey = JourneyFactory.GetL0Journey(Client.Home());

            var acceptedPage = journey.ApplyForLoan(200, 10)
                                     .FillPersonalDetails(Get.EnumToString(RiskMask.TESTEmployedMask))
                                     .FillAddressDetails()
                                     .FillAccountDetails()
                                     .FillBankDetails()
                                     .FillCardDetails()
                                     .WaitForAcceptedPage().CurrentPage as AcceptedPage;

            Assert.IsTrue(acceptedPage.IsAgreementFormDisplayed());

        }

        [Test, AUT(AUT.Uk), JIRA("UK-731")]
        public void LoanCompletionConfirmed()
        {
            string expectedDealDoneText = "Your application has been accepted\r\nThe cash will be winging its way into your bank account in the next 15 minutes! Please just be aware that different banks take different lengths of time to show new deposits.\n\nPlease don't forget that you have promised to repay on {repay date} when you'll need to have £{repay amount} ready in the bank account linked to your debit card. You can login to your Wonga account at any time to keep track of your loan, apply for more cash (depending on your trust rating) and even extend or repay early.\n\nWe hope you find the money useful and, if you love our service, please now check out the options below!";
            const int loanAmount = 100;
            const int days = 10;
            string paymentAmount = 115.91M.ToString("#.00");
            DateTime paymentDate = DateTime.Now.AddDays(days);

            var journey = JourneyFactory.GetL0Journey(Client.Home());

            var dealDonePage = journey.ApplyForLoan(loanAmount, days)
                                   .FillPersonalDetails(Get.EnumToString(RiskMask.TESTEmployedMask))
                                   .FillAddressDetails()
                                   .FillAccountDetails()
                                   .FillBankDetails()
                                   .FillCardDetails()
                                   .WaitForAcceptedPage()
                                   .FillAcceptedPage().CurrentPage as DealDonePage;

            string actualDealDoneText = dealDonePage.GetDealDonePageText;

            // Check text on the Deal Done page is displayed correctly

            expectedDealDoneText = expectedDealDoneText.Replace("{repay date}", GetOrdinalDate(paymentDate)).Replace("{repay amount}", paymentAmount);
            Assert.AreEqual(expectedDealDoneText, actualDealDoneText);
            //Assert.Contains(actualDealDoneText, "The cash will be winging its way into your bank account in the next 15 minutes!");
            //Assert.Contains(actualDealDoneText, paymentAmount);
            //Assert.Contains(actualDealDoneText, GetOrdinalDate(paymentDate));
            //Assert.Contains(actualDealDoneText, paymentDate.ToString("d"));
            //Assert.Contains(actualDealDoneText, paymentDate.ToString("MMM"));
            //Assert.Contains(actualDealDoneText, paymentDate.ToString("yyyy"));
            //Assert.Contains(actualDealDoneText, paymentDate.ToString("dddd"));
        }

        public string GetOrdinalDate(DateTime date)
        {
            //Returns date as string in the format "Wed 18th Apr 2012"
            var cDate = date.Day.ToString("d")[date.Day.ToString("d").Length - 1];
            string suffix;
            switch (cDate)
            {
                case '1':
                    suffix = "st";
                    break;
                case '2':
                    suffix = "nd";
                    break;
                case '3':
                    suffix = "rd";
                    break;
                default:
                    suffix = "th";
                    break;
            }
            var sDate = " " + date.Day.ToString("d") + " ";
            var sDateOrdinial = " " + date.Day.ToString("d") + suffix + " ";
            return date.ToString("dddd d MMM yyyy").Replace(sDate, sDateOrdinial);
        }
    }
}
