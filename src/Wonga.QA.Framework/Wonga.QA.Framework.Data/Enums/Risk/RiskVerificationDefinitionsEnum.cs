﻿using System.ComponentModel;

namespace Wonga.QA.Framework.Data.Enums.Risk
{
    public enum RiskVerificationDefinitions
    {
        [Description("CreditBureauEcbsScoreIsAcceptableVerification")] CreditBureauEcbsScoreIsAcceptableVerification,
        [Description("CreditBureauDataIsAvailableVerification")] CreditBureauDataIsAvailableVerification,
        [Description("CreditBureauCustomerIsSolventVerification")] CreditBureauCustomerIsSolventVerification,
        [Description("CreditBureauCustomerIsAliveVerification")] CreditBureauCustomerIsAliveVerification,
        [Description("DateOfBirthIsCorrectVerification")] DateOfBirthIsCorrectVerification,
        [Description("GraydonBusinessIsTradingVerification")] GraydonBusinessIsTradingVerification,
        [Description("CreditBureauCifasFraudCheckVerification")] CreditBureauCifasFraudCheckVerification,
        [Description("BusinessDataAvailableInGraydonVerification")] BusinessDataAvailableInGraydonVerification,
        [Description("CustomerIsEmployedVerification")] CustomerIsEmployedVerification,
        [Description("MonthlyIncomeVerification")] MonthlyIncomeVerification,
        [Description("MobilePhoneIsUniqueVerification")] MobilePhoneIsUniqueVerification,
        [Description("SuspiciousActivityVerification")] SuspiciousActivityVerification,
        [Description("BlackListVerification")] BlackListVerification,
        [Description("IovationAutoReviewVerification")] IovationAutoReviewVerification,
        [Description("IovationVerification")] IovationVerification,
        [Description("DebtSaleVerification")] DebtSaleVerification,
        [Description("FraudBlacklistVerification")] FraudBlacklistVerification,
        [Description("FraudAutomatedVerification")] FraudAutomatedVerification,
        [Description("RepaymentVerification")] RepaymentVerification,
        [Description("CardPaymentPaymentCardIsValidVerification")] CardPaymentPaymentCardIsValidVerification,
        [Description("CallValidateBankAccountVerification")] CallValidateBankAccountVerification,
        [Description("CallValidatePaymentCardIsValidVerification")] CallValidatePaymentCardIsValidVerification,
        [Description("FraudManualVerification")] FraudManualVerification,
        [Description("ExperianPaymentCardIsValidVerification")] ExperianPaymentCardIsValidVerification,
        [Description("ExperianBankAccountVerification")] ExperianBankAccountVerification,
        [Description("UserAssistBankAccountVerification")] UserAssistBankAccountVerification,
        [Description("GraydonPaymentScoreVerification")] GraydonPaymentScoreVerification,
        [Description("GraydonAugurScoreVerification")] GraydonAugurScoreVerification,
        [Description("LiveIdvDobIsCorrectVerification")] LiveIdvDobIsCorrectVerification,
        [Description("MainApplicantMatchesBusinessBureauDataVerification")] MainApplicantMatchesBusinessBureauDataVerification,
        [Description("MainApplicantDurationAcceptableVerification")] MainApplicantDurationAcceptableVerification,
        [Description("BusinessDateOfIncorporationAcceptableVerification")] BusinessDateOfIncorporationAcceptableVerification,
        [Description("NumberOfDirectorsMatchesBusinessBureauDataVerification")] NumberOfDirectorsMatchesBusinessBureauDataVerification,
        [Description("BankAccountVerification")] BankAccountVerification,
        [Description("GuarantorNamesMatchBusinessBureauDataVerification")] GuarantorNamesMatchBusinessBureauDataVerification,
        [Description("BusinessGeneralManualVerification")] BusinessGeneralManualVerification,
		[Description("DoNotRelendVerification")] DoNotRelendVerification,
		[Description("ReputationPredictionPositiveVerification")] ReputationPredictionPositiveVerification,
		[Description("RepaymentPredictionPositiveVerification")] RepaymentPredictionPositiveVerification,
        [Description("ApplicantIsNotMinorVerification")] ApplicantIsNotMinorVerification,
        [Description("BusinessLoanApplicantIsNotOnBlackListVerification")] BusinessLoanApplicantIsNotOnBlackListVerification,
        [Description("CallValidateAndExperianBankAccountVerification")] CallValidateAndExperianBankAccountVerification,
        
    }
}
