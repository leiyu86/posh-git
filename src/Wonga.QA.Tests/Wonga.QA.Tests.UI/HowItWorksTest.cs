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
    [Parallelizable(TestScope.All)]
    class HowItWorksTest : UiTest
    {
        [Test, AUT(AUT.Wb), JIRA("QA-252")]
        public void HowItWorksOnWongaBusinessShouldHasTheCorrectDataDisplayed()
        {
            #region verification
            var verificationText = "We have already provided millions of loans to consumers and now we’re helping the UK’s small businesses solve their short term cash flow needs. Applying for a Wonga Business loan is simple. We don’t do paperwork! Unlike a bank, we don’t require your business or marketing plans and you won’t need to meet us face to face. Our unique service is objective and completely online from start to finish. First of all, use our sliders to decide exactly how much cash the business wants to borrow and how long you need it for. We'll then ask for some details about you and the business for our speedy credit checks and on-screen decision. On acceptance we will show you the full costs and weekly repayment quote of the loan and, once you're happy with everything, you can proceed. We usually provide an immediate answer and, if approved, you could have cash deposited in your business bank account within one business day. Depending on who you bank with, it could be as little as 15 minutes. This speed, combined with our unparalleled flexibility, makes us the most convenient business lender in the land! The chosen weekly repayment amount is collected via continuous payment authority using your business debit card on the seventh day after the cash advance. Repayments then continue on a weekly basis for the agreed term, so you just need to ensure the required funds are always available in the associated business bank account. If we cannot collect the amount you owe at first, we may make further attempts to collect the repayment using your business debit card. The business can initially apply for any amount from £3,000 to £10,000. Should you choose to use Wonga Business more than once - and provided you use the service responsibly - we may increase your business trust rating, giving your business more borrowing flexibility in the future. You are always free to make an early repayment to save on interest. You can even repay in full, at any time, with no hidden fees. Once you have a Wonga Business account, you'll find the application process is even faster next time too. If you're still feeling a bit foggy, there's further introductory information below, or check out our FAQ page for answers to most specific questions.";
            var verificationTransparencyHeader = "Transparency";
            var verificationTransparencyText = "Apply online for an instant decision, the costs involved and a weekly repayment quote. We will always tell you once your application is accepted what the weekly repayments are, how many repayments need to be made and what the full cost of repayment will be. The interest rate we offer will depend on our assessment of the risk profile of your business. There are no catches or extra costs to worry about, providing you stick to your side of the deal.";
            var verificationPersonalGuaranteeHeader = "Personal guarantee";
            var verificationPersonalGuaranteeText =
                "All directors or partners involved will be required to provide a personal guarantee, which will be binding once the loan agreement has been made. Under this guarantee, each director or partner may have to pay instead of the business if the business fails to keep to its side of the agreement (but you will not be liable for more than the business would have had to pay). We will ask for debit card details and authority to use the card to collect the amount owed if the business does not pay. Please ensure that you fully understand your obligations before entering into the guarantee. You may wish to take independent legal advice.";
            var verificationFailureToStickHeader = "Failure to stick to your side of the deal";
            var verificationFailureToStickText = "Failure to stick to your side of the deal The only way costs will mount beyond our initial calculation is if the business doesn’t keep its promise. In other words, if there are not enough funds available each week for our automatic debit card collection. We will apply a default fee of £10 if a repayment is missed as well as anytime a repayment fails. Please note your bank may also charge you for each failed collection. If a second repayment fails we will also attempt to recover the outstanding loan repayments from each of or all of the guarantors. Interest will also continue to accrue on the balance if we can't reach a fair agreement in the meantime. And if the business doesn’t work with us and we can't recover the money over a reasonable period, the account may be passed to an external partner who will attempt to recover the funds from the business and from any personal guarantors. Continued failure to repay a loan will also mean credit reference agencies (CRAs) will record the outstanding debt. This information may be supplied to other organisations by CRAs and fraud protection agencies to perform similar checks and recover debts that are owed. Records remain on file for six years after they are closed, whether settled or defaulted. The debt may be sold. Finally, your business trust rating will be hit hard and your business probably won't be able to borrow from us again in the future. None of these things are worth risking, so if you have doubts over whether your business will be able to repay a loan comfortably, please don't apply in the first place. We use award winning technology and all the public data we can get our hands on to make the best lending decisions possible, but we also urge you to consider the serious nature of the commitment before applying.";
            #endregion
            var howitworks = Client.HowItWorks();
            Assert.AreEqual(verificationText, howitworks.GetTextFromPage.Replace("\r\n", " "));
            Assert.AreEqual(verificationTransparencyHeader, howitworks.GetTransparencyHeader);
            Assert.AreEqual(verificationTransparencyText, howitworks.GetTransparencyText);
            Assert.AreEqual(verificationPersonalGuaranteeHeader, howitworks.GetPersonalGuaranteeHeader);
            Assert.AreEqual(verificationPersonalGuaranteeText, howitworks.GetPersonalGuaranteeText);
            Assert.AreEqual(verificationFailureToStickHeader, howitworks.GetFailureToStickHeader);
            Assert.AreEqual(verificationFailureToStickText, howitworks.GetFailureToStickText.Replace("\r\n", " "));
        }
    }
}
