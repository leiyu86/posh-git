﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI;
using Wonga.QA.Framework.UI.UiElements.Pages.Common;
using Wonga.QA.Framework.UI.UiElements.Pages.Wb;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.Ui
{
    class DealDonePageTest : UiTest
    {
        private int _amountMax;
        private int _amountMin;
        private int _termMax;
        private int _termMin;
        private string _repaymentDate;
        private ApiResponse _response;
        private DateTime _actualDate;

        [SetUp, JIRA("QA-192")]
        public void GetInitialValues()
        {
            ApiRequest request;
            switch (Config.AUT)
            {
                case AUT.Uk:
                    request = new GetFixedTermLoanOfferUkQuery();
                    break;
                case AUT.Za:
                    request = new GetFixedTermLoanOfferZaQuery();
                    break;
                case AUT.Ca:
                    request = new GetFixedTermLoanOfferCaQuery();
                    break;
                default:
                    throw new NotImplementedException();
            }
            _response = Driver.Api.Queries.Post(request);
            _amountMax = (int)Decimal.Parse(_response.Values["AmountMax"].Single(), CultureInfo.InvariantCulture);
            _amountMin = (int)Double.Parse(_response.Values["AmountMin"].Single(), CultureInfo.InvariantCulture);
            _termMax = Int32.Parse(_response.Values["TermMax"].Single(), CultureInfo.InvariantCulture);
            _termMin = Int32.Parse(_response.Values["TermMin"].Single(), CultureInfo.InvariantCulture);
        }

        [Test, AUT(AUT.Za), JIRA("QA-192")]
        public void CorrectDataShouldBeDisplayedOnApplicationSuccessPageForZa()
        {
            int randomAmount = _amountMin + (new Random()).Next(_amountMax - _amountMin);
            int randomDuration = _termMin + (new Random()).Next(_termMax - _termMin);

            var journey = new Journey(Client.Home());
            var processingPage = journey.ApplyForLoan(randomAmount, randomDuration)
                                 .FillPersonalDetails(Data.EnumToString(RiskMask.TESTEmployedMask))
                                 .FillAddressDetails()
                                 .FillAccountDetails()
                                 .FillBankDetails()
                                 .CurrentPage as ProcessingPage;

            var acceptedPage = processingPage.WaitFor<AcceptedPage>() as AcceptedPage;
            acceptedPage.SignAgreementConfirm();
            acceptedPage.SignDirectDebitConfirm();
            var dealDone = acceptedPage.Submit() as DealDonePage;
            // Check date
            DateTime _date = DateTime.Parse(dealDone.GetRepaymentDate());
            _actualDate = DateTime.Now.AddDays(randomDuration);
            Assert.AreEqual(String.Format(CultureInfo.InvariantCulture, "{0:d MMM yyyy}", _date), String.Format(CultureInfo.InvariantCulture, "{0:d MMM yyyy}", _actualDate));
            // Check amount
            _response = Driver.Api.Queries.Post(new GetFixedTermLoanCalculationZaQuery { LoanAmount = randomAmount, Term = randomDuration });
            string totalRepayable = _response.Values["TotalRepayable"].Single();
            Assert.AreEqual(dealDone.GetRapaymentAmount().Remove(0, 1), totalRepayable);
        }

        [Test, AUT(AUT.Ca), JIRA("QA-192"), Pending("CA WIP,RC FE seems broken - postponing the push of the selenium tests")]
        public void CorrectDataShouldBeDisplayedOnApplicationSuccessPageForCa()
        {
            int randomAmount = _amountMin + (new Random()).Next(_amountMax - _amountMin);
            int randomDuration = _termMin + (new Random()).Next(_termMax - _termMin);

            var journey = new Journey(Client.Home());
            var processingPage = journey.ApplyForLoan(randomAmount, randomDuration)
                                 .FillPersonalDetails(Data.EnumToString(RiskMask.TESTEmployedMask))
                                 .FillAddressDetails()
                                 .FillAccountDetails()
                                 .FillBankDetails()
                                 .CurrentPage as ProcessingPage;

            var acceptedPage = processingPage.WaitFor<AcceptedPage>() as AcceptedPage;
            acceptedPage.SignConfirmCA(DateTime.Now.ToString("d MMM yyyy"), _firstName, _lastName);
            var dealDone = acceptedPage.Submit() as DealDonePage;
            // Check data

            // Check amount

        }
    }
}