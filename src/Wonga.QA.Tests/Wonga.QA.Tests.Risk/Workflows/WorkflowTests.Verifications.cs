﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Db.Extensions;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.Risk.Workflows
{
	public partial class WorkflowTests
	{
		#region Verifications

		private readonly List<string> ExpectedVerificationNamesL0 = GetExpectedVerificationNamesL0();
		private readonly List<string> ExpectedVerificationNamesLn = GetExpectedVerificationNamesLn();

		private static readonly List<string> ExpectedVerificationNamesL0Za = new List<string>()
		                                                             	{
		                                                             		"CustomerIsEmployedVerification",
		                                                             		"MonthlyIncomeVerification",
		                                                             		"ApplicationTermNotLessThan4DaysVerification",
		                                                             		"MobilePhoneIsUniqueVerification",
		                                                             		"BlackListVerification",
		                                                             		"IovationAutoReviewVerification",
		                                                             		"IovationVerification",
		                                                             		"DoNotRelendVerification",
		                                                             		"FraudBlacklistVerification",
		                                                             		"ReputationPredictionVerification",
		                                                             		"CreditBureauDataIsAvailableVerification",
		                                                             		"CustomerNameIsCorrectVerification",
		                                                             		"ApplicantIsSolventNoticeVerification",
		                                                             		"ApplicantIsSolventVerification",
		                                                             		"CustomerIsAliveVerification",
		                                                             		"RepaymentPredictionVerification",
		                                                             		"BankAccountIsValidVerification"
		                                                             	};

		private static readonly List<string> ExpectedVerificationNamesLnZa = new List<string>()
		                                                             	{
		                                                             		"CustomerIsEmployedVerification",
		                                                             		"MonthlyIncomeVerification",
		                                                             		"ApplicationTermNotLessThan4DaysVerification",
		                                                             		"BlackListVerification",
		                                                             		"IovationAutoReviewVerification",
		                                                             		"IovationVerification",
		                                                             		"DoNotRelendVerification",
		                                                             		"FraudBlacklistVerification",
		                                                             		"CreditBureauDataIsAvailableVerification",
		                                                             		"ApplicantIsSolventNoticeVerification",
		                                                             		"ApplicantIsSolventVerification",
		                                                             		"CustomerIsAliveVerification",
		                                                             		"RepaymentPredictionVerification",
		                                                             		"BankAccountIsValidVerification"
		                                                             	};

		#endregion

		[Test, AUT(AUT.Za)]
		public void WorkflowCorrectVerificationsUsedL0()
		{
			var customer = CustomerBuilder.New().WithEmployer("Wonga").Build();
			var application =
				ApplicationBuilder.New(customer)
				.WithExpectedDecision(ApplicationDecisionStatus.Declined)
				.Build();

			var actualVerificationNames = Drive.Db.GetVerificationDefinitionsForApplication(application.Id).Select(a => a.Name);
			Assert.AreElementsEqual(ExpectedVerificationNamesL0, actualVerificationNames);
		}

		[Test, AUT(AUT.Za)]
		public void WorkflowCorrectVerificationsUsedLn()
		{
			var customer = CustomerBuilder.New().Build();
			ApplicationBuilder.New(customer).Build().RepayOnDueDate();

			Drive.Db.UpdateEmployerName(customer.Id, "Wonga");

			var application =
				ApplicationBuilder.New(customer)
				.WithExpectedDecision(ApplicationDecisionStatus.Declined)
				.Build();

			var actualVerificationNames = Drive.Db.GetVerificationDefinitionsForApplication(application.Id).Select(a => a.Name);
			Assert.AreElementsEqual(ExpectedVerificationNamesLn, actualVerificationNames);
		}

		#region Helpers

		private static List<string> GetExpectedVerificationNamesL0()
		{
			switch (Config.AUT)
			{
				case AUT.Za:
					{
						return ExpectedVerificationNamesL0Za;
					}
					break;

				default:
					{
						throw new NotImplementedException(Config.AUT.ToString());
					}
			}
		}

		private static List<string> GetExpectedVerificationNamesLn()
		{
			switch (Config.AUT)
			{
				case AUT.Za:
					{
						return ExpectedVerificationNamesLnZa;
					}
					break;

				default:
					{
						throw new NotImplementedException(Config.AUT.ToString());
					}
			}
		}
		#endregion
	}
}