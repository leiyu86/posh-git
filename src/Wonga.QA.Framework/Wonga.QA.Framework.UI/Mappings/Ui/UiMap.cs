﻿using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.UI.Mappings.Content;
using Wonga.QA.Framework.UI.Mappings.Elements;
using Wonga.QA.Framework.UI.Mappings.Pages;
using Wonga.QA.Framework.UI.Mappings.Pages.PayLater;
using Wonga.QA.Framework.UI.Mappings.Pages.Wb;
using Wonga.QA.Framework.UI.Mappings.Sections;
using Wonga.QA.Framework.UI.Mappings.Pages.SalesForce;
using Wonga.QA.Framework.UI.Mappings.Ui.Elements;
using AboutUsPage = Wonga.QA.Framework.UI.Mappings.Pages.AboutUsPage;
using Wonga.QA.Framework.UI.Ui.Pages.Common;
using AccountDetailsSection = Wonga.QA.Framework.UI.Mappings.Sections.AccountDetailsSection;
using AddressDetailsPage = Wonga.QA.Framework.UI.Mappings.Pages.AddressDetailsPage;
using BusinessSummaryPage = Wonga.QA.Framework.UI.Mappings.Pages.BusinessSummaryPage;
using ExtensionAgreementPage = Wonga.QA.Framework.UI.Mappings.Pages.ExtensionAgreementPage;
using HelpElement = Wonga.QA.Framework.UI.Mappings.Elements.HelpElement;
using HomePage = Wonga.QA.Framework.UI.Mappings.Pages.HomePage;
using MobilePinVerificationSection = Wonga.QA.Framework.UI.Mappings.Sections.MobilePinVerificationSection;
using MyPaymentsPage = Wonga.QA.Framework.UI.Mappings.Pages.MyPaymentsPage;
using MySummaryPage = Wonga.QA.Framework.UI.Mappings.Pages.MySummaryPage;
using PayLaterLoginPage = Wonga.QA.Framework.UI.Mappings.Pages.PayLaterLoginPage;
using RepayDueFullpaySuccessPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayDueFullpaySuccessPage;
using RepayDuePartpaySuccessPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayDuePartpaySuccessPage;
using RepayDuePaymentFailedPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayDuePaymentFailedPage;
using RepayEarlyFullpaySuccessPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayEarlyFullpaySuccessPage;
using RepayEarlyPartpaySuccessPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayEarlyPartpaySuccessPage;
using RepayEarlyPaymentFailedPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayEarlyPaymentFailedPage;
using RepayOverdueFullpaySuccessPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayOverdueFullpaySuccessPage;
using RepayOverduePartpaySuccessPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayOverduePartpaySuccessPage;
using RepayOverduePaymentFailedPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayOverduePaymentFailedPage;
using RepayProcessingPage = Wonga.QA.Framework.UI.Mappings.Pages.RepayProcessingPage;
using TopupProcessingPage = Wonga.QA.Framework.UI.Mappings.Pages.TopupProcessingPage;
using ExtensionProcessingPage = Wonga.QA.Framework.UI.Mappings.Pages.ExtensionProcessingPage;
using YourDetailsSection = Wonga.QA.Framework.UI.Mappings.Sections.YourDetailsSection;
using TopupDealDonePage = Wonga.QA.Framework.UI.Mappings.Pages.TopupDealDonePage;
using ExtensionErrorPage = Wonga.QA.Framework.UI.Mappings.Pages.ExtensionErrorPage;


namespace Wonga.QA.Framework.UI.Mappings
{
    public class UiMap
    {
        private static UiMap MyElements;
        private static object _lock = new object();

        public static UiMap Get
        {
            get
            {
                lock (_lock)
                {
                    if (MyElements == null)
                    {
                        MyElements = new UiMap();
                    }
                }
                return MyElements;
            }
        }

        protected UiMap()
        {
            XmlMapper = new XmlMapper("Wonga.QA.Framework.UI.Mappings.Xml.Ui._base.xml", string.Format("Wonga.QA.Framework.UI.Mappings.Xml.Ui.{0}.xml", Config.AUT));
            XmlMapper.GetValues(this, null);
        }

        public XmlMapper XmlMapper = null;

        #region Section
        public virtual YourNameSection YourNameSection { get; set; }
        public virtual YourDetailsSection YourDetailsSection { get; set; }
        public virtual MobilePinVerificationSection MobilePinVerificationSection { get; set; }
        public virtual ContactingYouSection ContactingYouSection { get; set; }
        public virtual AccountDetailsSection AccountDetailsSection { get; set; }
        public virtual DebitCardSection DebitCardSection { get; set; }
        public virtual BankAccountSection BankAccountSection { get; set; }
        public virtual EmploymentDetailsSection EmploymentDetailsSection { get; set; }
        public virtual ProvinceSection ProvinceSection { get; set; }
        public virtual MyAccountNavigationSection MyAccountNavigationSection { get; set; }
        public virtual ApplicationSection ApplicationSection { get; set; }
        public virtual AddressDetailsSection AddressDetailsSection { get; set; }
        public virtual PreviousAddresDetailsSection PreviousAddresDetailsSection { get; set; }
        #endregion

        #region Elements
        public virtual SlidersElement SlidersElement { get; set; }
        public virtual LoginElement LoginElement { get; set; }
        public virtual HelpElement HelpElement { get; set; }
        public virtual InternationalElement InternationalElement { get; set; }
        public virtual FAQElement FAQElement { get; set; }
        public virtual SurveyElement SurveyElement { get; set; }
        public virtual ContactElement ContactElement { get; set; }
        public virtual TabsElement TabsElement { get; set; }
        public virtual TopupSlidersElement TopupSlidersElement { get; set; }
        public virtual SmallTopupSlidersElement SmallTopupSlidersElement { get; set; }
        public virtual SmallExtensionSlidersElement SmallExtensionSlidersElement { get; set; }
        public virtual ChangeMyAddressElement ChangeMyAddressElement { get; set; }
        public virtual SmallRepaySlidersElement SmallRepaySlidersElement { get; set; }
        public virtual TabsElementMobile TabsElementMobile { get; set; }
        public virtual HomePageWelcomePopup HomePageWelcomePopup { get; set; }

        #endregion

        #region WbPages

        public virtual EligibilityQuestionsPage EligibilityQuestionsPage { get; set; }
        public virtual BusinessDetailsPage BusinessDetailsPage { get; set; }
        public virtual AdditionalDirectorsPage AdditionalDirectorsPage { get; set; }
        public virtual AddAditionalDirectorsPage AddAditionalDirectorsPage { get; set; }
        public virtual BusinessDebitCardPage BusinessDebitCardPage { get; set; }
        public virtual ReferPage ReferPage { get; set; }

        #endregion

        #region CommonPages

        public virtual PersonalBankAccountDetailsPage PersonalBankAccountPage { get; set; }
        public virtual PersonalDebitCardPage PersonalDebitCardDetailsPage { get; set; }
        public virtual AcceptedPage AcceptedPage { get; set; }
        public virtual ApplyPage ApplyPage { get; set; }
        public virtual PersonalDetailsPage PersonalDetailsPage { get; set; }
        public virtual ProcessingPage ProcessingPage { get; set; }
        public virtual DealDonePage DealDonePage { get; set; }
        public virtual DeclinedPage DeclinedPage { get; set; }
        public virtual AddressDetailsPage AddressDetailsPage { get; set; }
        public virtual AccountDetailsPage AccountDetailsPage { get; set; }
        public virtual BankAccountPage BankAccountPage { get; set; }
        public virtual DebitCardPage DebitCardPage { get; set; }
        public virtual LoginPage LoginPage { get; set; }
        public virtual ForgotPasswordPage ForgotPasswordPage { get; set; }
        public virtual HomePage HomePage { get; set; }
        public virtual MySummaryPage MySummaryPage { get; set; }
        public virtual MyPaymentsPage MyPaymentsPage { get; set; }
        public virtual MyPersonalDetailsPage MyPersonalDetailsPage { get; set; }
        public virtual JargonBusterPage JargonBusterPage { get; set; }
        public virtual AboutUsPage AboutUsPage { get; set; }
        public virtual HowItWorksPage HowItWorksPage { get; set; }
        public virtual BlogPage BlogPage { get; set; }
        public virtual OurCustomersPage OurCustomersPage { get; set; }
        public virtual ResponsibleLendingPage ResponsibleLendingPage { get; set; }
        public virtual WhyUseUsPage WhyUseUsPage { get; set; }
        public virtual TopupAgreementPage TopupAgreementPage { get; set; }
        public virtual TopupDealDonePage TopupDealDonePage { get; set; }
        public virtual TopupProcessingPage TopupProcessingPage { get; set; }
        public virtual TopupRequestPage TopupRequestPage { get; set; }
        public virtual ApplyTermsPage ApplyTermsPage { get; set; }
        public virtual ExtensionPaymentFailedPage ExtensionPaymentFailedPage { get; set; }
        public virtual ExtensionAgreementPage ExtensionAgreementPage { get; set; }
        public virtual ExtensionDealDonePage ExtensionDealDonePage { get; set; }
        public virtual ExtensionErrorPage ExtensionErrorPage { get; set; }
        public virtual ExtensionRequestPage ExtensionRequestPage { get; set; }
        public virtual ExtensionProcessingPage ExtensionProcessingPage { get; set; }
        public virtual RepayRequestPage RepayRequestPage { get; set; }
        public virtual RepayProcessingPage RepayProcessingPage { get; set; }
        public virtual FAQPage FAQPage { get; set; }
        public virtual RepayEarlyPaymentFailedPage RepayEarlyPaymentFailedPage { get; set; }
        public virtual RepayDuePaymentFailedPage RepayDuePaymentFailedPage { get; set; }
        public virtual RepayOverduePaymentFailedPage RepayOverduePaymentFailedPage { get; set; }
        public virtual RepayEarlyPartpaySuccessPage RepayEarlyPartpaySuccessPage { get; set; }
        public virtual RepayDuePartpaySuccessPage RepayDuePartpaySuccessPage { get; set; }
        public virtual RepayOverduePartpaySuccessPage RepayOverduePartpaySuccessPage { get; set; }
        public virtual RepayEarlyFullpaySuccessPage RepayEarlyFullpaySuccessPage { get; set; }
        public virtual RepayDueFullpaySuccessPage RepayDueFullpaySuccessPage { get; set; }
        public virtual RepayOverdueFullpaySuccessPage RepayOverdueFullpaySuccessPage { get; set; }
        public virtual RepaymentOptionsPage RepaymentOptionsPage { get; set; }
        public virtual EasypaymentNumberPrintPage EasypaymentNumberPrintPage { get; set; }
        public virtual MySummaryPageMobile MySummaryPageMobile { get; set; }
        public virtual BusinessSummaryPage BusinessSummaryPage { get; set; }
        #endregion

        #region SalesForcePages

        public virtual SalesForceLoginPage SalesForceLoginPage { get; set; }
        public virtual SalesForceHomePage SalesForceHomePage { get; set; }
        public virtual SalesForceSearchResultPage SalesForceSearchResultPage { get; set; }
        public virtual SalesForceCustomerDetailPage SalesForceCustomerDetailPage { get; set; }
        
        #endregion

        #region PayLater

        public virtual PayLaterLoginPage PayLaterLoginPage { get; set; }
        public virtual SubmitionPage SubmitionPage { get; set; }

        #endregion


        #region PrePaid

        public virtual PrepaidCardPage PrepaidCardPage { get; set; }
        public virtual PrepaidRegisterDetailsPage PrepaidRegisterDetailsPage { get; set; }

        #endregion

        #region AdminPages

        public virtual PaymentCardsPage PaymentCardsPage { get; set; }
        public virtual AddCardPage AddCardPage { get; set; }
        public virtual AccountingPage AccountingPage { get; set; }
        public virtual CashOutPage CashOutPage { get; set; }

        #endregion
    }
}
