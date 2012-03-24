﻿
namespace Wonga.QA.Framework.Api
{
    public enum RiskMask
    {
        TESTTransUnionandBank,
        TESTTransUnion,
        TESTEmployedMask,
        TESTCardMask,
        TESTCardBankMask,
        TESTAll,
        TESTExcludeVerification,
        TESTBlacklist,
        TESTIsAlive,
        TESTDateOfBirth,
        TESTIsSolvent,
        TESTMonthlyIncome,
        TESTCustomerHistoryIsAcceptable,
        TESTApplicationElementNotOnBlacklist,
        TESTBankAccountMatchedToApplicant,
        TESTDirectFraud,
        TESTApplicationElementNotCIFASFlagged,
        TESTCreditBureauDataIsAvailable,
        TESTApplicantIsNotDeceased,
        TESTCustomerIsEmployed,
        TESTApplicantIsSolvent,
        TESTCustomerDateOfBirthIsCorrect,
        TESTCustomerDateOfBirthIsCorrectSME,
        TESTFraudScorePositive,
        TESTDirectFraudCheck,
        TESTCreditBureauScoreIsAcceptable,
        TESTApplicationElementNotOnCSBlacklist,
        TESTApplicationDeviceNotOnBlacklist,
        TESTDeviceNotOnBlacklist,
        TESTMonthlyIncomeEnoughForRepayment,
        TESTPaymentCardIsValid,
        TESTRepaymentPredictionPositive,
		TESTReputationtPredictionPositive,
        TESTNoSuspiciousApplicationActivity,
        TESTCallValidateBankAccountMatchedToApplicant,
        TESTCallValidatePaymentCardIsValid,
        TESTExperianBankAccountMatchedToApplicant,
        TESTExperianPaymentCardIsValid,
        TESTRiskBankAccountMatchedToApplicant,
        TESTRiskPaymentCardIsValid,
        TESTRiskFraudScorePositive,
        TESTExperianCreditBureauDataIsAvailable,
        TESTExperianApplicationElementNotCIFASFlagged,
        TESTExperianApplicantIsNotDeceased,
        TESTExperianApplicantIsSolvent,
        TESTExperianCustomerDateOfBirthIsCorrect,
        TESTManualReferralIovation,
        TESTManualReferralCIFAS,
        TESTManualReferralFraudScore,
        TESTCustomerNameIsCorrect,
        TESTMobilePhoneIsUnique,
        TESTApplicantIsNotMinor,
        TESTBankAccountIsValid,
        TESTEquifaxCreditBureauDataIsAvailable,
        TESTHomePhoneIsAcceptable,
        TESTBusinessPaymentScoreIsAcceptable,
        TESTBusinessIsCurrentlyTrading,
        TESTBusinessBureauDataIsAvailable,
        TESTMainApplicantMatchesBusinessBureauData,
        TESTBusinessPerformanceScoreIsAcceptaple,
        TESTMainApplicantDurationAcceptable,
        TESTNumberOfDirectorsMatchesBusinessBureauData,
        TESTBusinessDateOfIncorporationAcceptable,
        TESTNoCheck,
        TESTTooManyLoansAtAddress,
        TESTGuarantorNamesMatchBusinessBureauData
    }
}
