﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Api.Exceptions;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Db.Ops;
using Wonga.QA.Framework.Msmq;
using Wonga.QA.Tests.Core;
using System.Threading;

namespace Wonga.QA.Tests.Payments.Command
{
	public class GenerateRepaymentNumberTest
	{
		private const int delay = 15000;
		private dynamic _repaymentAccount = Drive.Data.Payments.Db.RepaymentAccount;

		[FixtureSetUp]
		public void FixtureSetUp()
		{
			
		}

		[FixtureTearDown]
		public void FixtureTearDown()
		{
			
		}

		[Test, AUT(AUT.Za), JIRA("ZA-2290")]
		public void GenerateRepaymentNumber_ShouldSaveRepaymentNumberToAccountRepayment()
		{
			//Arrange
			var customer = CustomerBuilder.New().Build();
			var app = ApplicationBuilder.New(customer).WithExpectedDecision(ApplicationDecisionStatus.Accepted).Build();

			var generateRepaymentNumberCommand = new Framework.Cs.GenerateRepaymentNumberCommand
			{
				AccountId = app.AccountId
			};

			//Act
			Drive.Cs.Commands.Post(generateRepaymentNumberCommand);

			Thread.Sleep(delay);

			//Assert
			var repaymentAccount = Do.Until(() => _repaymentAccount.FindAll(_repaymentAccount.AccountId == app.AccountId)
													.FirstOrDefault());
			Assert.IsNotNull(repaymentAccount);
			Assert.IsNotNull(repaymentAccount.RepaymentNumber);

		}


		[Test, AUT(AUT.Za), JIRA("ZA-2290")]
		[ExpectedException(typeof(ValidatorException), "Payments_GenerateRepaymentNumber_RepaymentNumberForAccountAlreadyExists")]
		public void GenerateRepaymentNumber_ForAccountWithExistingRepaymentNumber_ExpectValidationException()
		{
			//Arrange
			var customer = CustomerBuilder.New().Build();
			var app = ApplicationBuilder.New(customer).WithExpectedDecision(ApplicationDecisionStatus.Accepted).Build();

			var generateRepaymentNumberCommand = new Framework.Cs.GenerateRepaymentNumberCommand
			{
				AccountId = app.AccountId
			};

			Drive.Cs.Commands.Post(generateRepaymentNumberCommand);
			Thread.Sleep(5000);

			//Act
			Drive.Cs.Commands.Post(generateRepaymentNumberCommand);
		}

		[Test, AUT(AUT.Za), JIRA("ZA-2290")]
		[ExpectedException(typeof(ValidatorException), "Payments_GenerateRepaymentNumber_AccountDoesNotExist")]
		public void GenerateRepaymentNumber_ForNonExistingAccount_ExpectValidationException()
		{
			var generateRepaymentNumberCommand = new Framework.Cs.GenerateRepaymentNumberCommand
			{
				AccountId = Guid.NewGuid()
			};

			//Act
			Drive.Cs.Commands.Post(generateRepaymentNumberCommand);
		}

	}
}