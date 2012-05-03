﻿using MbUnit.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI;
using Wonga.QA.Framework.UI.UiElements.Pages.Common;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.Ui.Mobile
{
    [TestFixture]
    public class L0LoanMobile : UiMobileTest
    {
        [Test, AUT(AUT.Za)]
        public void ZaAcceptedLoanMobile()
        {
            var journey = JourneyFactory.GetL0Journey(Client.MobileHome());
            var acceptedPage = journey.ApplyForLoan(200, 10)
                                 .FillPersonalDetails(Get.EnumToString(RiskMask.TESTEmployedMask))
                                 .FillAddressDetails()
                                 .FillAccountDetails()
                                 .FillBankDetails()
                                 .WaitForAcceptedPage()
                                 .CurrentPage as AcceptedPage;
            acceptedPage.SignAgreementConfirm();
            acceptedPage.SignDirectDebitConfirm();
            var dealDone = acceptedPage.Submit();
        }

        [Test, AUT(AUT.Za)]
        public void ZaDeclinedLoanMobile()
        {
            var journey = JourneyFactory.GetL0Journey(Client.MobileHome());
            var declinedPage = journey.ApplyForLoan(200, 10)
                                 .FillPersonalDetails()
                                 .FillAddressDetails()
                                 .FillAccountDetails()
                                 .FillBankDetails()
                                 .WaitForDeclinedPage()
                                 .CurrentPage as DeclinedPage;

        }

    }
}