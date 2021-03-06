﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Db;
using Wonga.QA.Framework.Mocks;
using Wonga.QA.Framework.Msmq;
using Wonga.QA.Tests.BankGateway.Enums;
using Wonga.QA.Tests.Core;
using Wonga.QA.Tests.Payments.Enums;
using Wonga.QA.Tests.Payments.Helpers;
using Wonga.QA.Tests.Payments.Helpers.Ca;
using IncomeFrequencyEnum = Wonga.QA.Framework.Api.Enums.IncomeFrequencyEnum;

namespace Wonga.QA.Tests.Payments
{
    [Parallelizable(TestScope.Self)]
	[Description("These tests can not be parallelized because it seems there is an issue in BGW mocks")]
    public class PadRepresentmentOptimization
    {
        private readonly dynamic _opsSagasPaymentsInArrears = Drive.Data.OpsSagas.Db.PaymentsInArrearsSagaEntity;
        private readonly dynamic _opsSagasMultipleRepresentmentsInArrearsSagaEntity =
                                                    Drive.Data.OpsSagas.Db.MultipleRepresentmentsInArrearsSagaEntity;
        private readonly dynamic _bgTrans = Drive.Data.BankGateway.Db.Transactions;
        private readonly dynamic _inArrearsNoticeSaga = Drive.Data.OpsSagas.Db.InArrearsNoticeSagaEntity;
		private readonly dynamic _mockedBankGatewayResponses = Drive.Data.QaData.Db.BankGatewayResponseSetups;
		private readonly dynamic _paymentTransactions = Drive.Data.Payments.Db.Transactions;
		private readonly dynamic _paymentApplications = Drive.Data.Payments.Db.Applications;

        [Test, AUT(AUT.Ca), JIRA("CA-1962"), FeatureSwitch(FeatureSwitchConstants.MultipleRepresentmentsInArrearsFeatureSwitchKey), Owner(Owner.TarasKudryavtsev)]
        public void WhenACustomerGoesIntoArrearsThenAnAttemptToRetrieve33PercentOfTheBalanceShouldBeMadeOnTheNextPayDate()
        {
            const double percentageToBeCollectedForRepresentmentOne = 0.33;
            const int loanTerm = 10;
            const decimal principle = 100;
            const decimal interest = 10;
            const decimal defaultCharge = 20;
            var customer = CustomerBuilder.New().Build();
            var application = ApplicationBuilder.New(customer).WithLoanTerm(loanTerm).Build();

            var nextPayDateForRepresentmentOne = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(DateTime.Today, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                                        (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentOne = (int)nextPayDateForRepresentmentOne.Subtract(DateTime.Today).TotalDays;

            application.PutIntoArrears((uint)numOfDaysToNextPayDateForRepresentmentOne);
			TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentOne);

            var arrearsInterestForRepresentmentOne =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principle + interest), numOfDaysToNextPayDateForRepresentmentOne));

            Assert.AreEqual(0,GetNumberOfRepresentmentsSent(application.Id));
            Assert.AreEqual(nextPayDateForRepresentmentOne.ToString(), GetNextRepresentmentDate(application.Id));

			WaitForProcessedMockedTransaction(customer.BankAccountNumber);
			TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
            WaitForSuccessfulCashInTransactions(application.Id, 1);

            var transactionForRepresentmentOne = _bgTrans.FindAll(_bgTrans.ApplicationId == application.Id &&
                                            _bgTrans.TransactionStatus == (int)BankGatewayTransactionStatus.Paid).
                               OrderByTransactionIdDescending().First();

            var amountToBeCollectedForRepresentmentOne =
                (decimal)(((double)(arrearsInterestForRepresentmentOne + principle + interest + defaultCharge)) * percentageToBeCollectedForRepresentmentOne);

            var amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentOne, 2, MidpointRounding.AwayFromZero);

			Assert.AreEqual(amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces, transactionForRepresentmentOne.Amount);
            Assert.AreEqual(amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces, CurrentRepresentmentAmount(application.Id));
            Assert.AreEqual(1,GetNumberOfRepresentmentsSent(application.Id));
            Assert.IsTrue(VerifyPaymentFunctions.VerifyDirectBankPaymentOfAmount(application.Id, -amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces));
        }

		[Test, AUT(AUT.Ca), JIRA("CA-1962"), FeatureSwitch(FeatureSwitchConstants.MultipleRepresentmentsInArrearsFeatureSwitchKey), Owner(Owner.TarasKudryavtsev)]
        public void WhenTheFirstPadRepresentmentForACustomerInArrearsFailsThenThereShouldBeNoMoreAttemptsToRetrieveMoneyFromTheCustomer()
        {
            const double percentageToBeCollectedForRepresentmentOne = 0.33;
            const int loanTerm = 10;
            const decimal principle = 100;
            const decimal interest = 10;
            const decimal defaultCharge = 20;
            var customer = CustomerBuilder.New().Build();
            var application = ApplicationBuilder.New(customer).WithLoanTerm(loanTerm).Build();

            var nextPayDateForRepresentmentOne = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(DateTime.Today, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                                        (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentOne = (int)nextPayDateForRepresentmentOne.Subtract(DateTime.Today).TotalDays;

            application.PutIntoArrears((uint)numOfDaysToNextPayDateForRepresentmentOne);
        	TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentOne);

            var arrearsInterestForRepresentmentOne =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principle + interest), numOfDaysToNextPayDateForRepresentmentOne));

            ScotiaResponseBuilder.New().
                ForBankAccountNumber(customer.BankAccountNumber).
				Reject();

            TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
        	WaitForFailedCashInTransactions(application.Id, customer.BankAccountNumber, 2);

            var transactionForRepresentmentOne = _bgTrans.FindAll(_bgTrans.ApplicationId == application.Id &&
                                            _bgTrans.TransactionStatus == (int)BankGatewayTransactionStatus.Failed).
                               OrderByTransactionIdDescending().First();

            var amountToBeCollectedForRepresentmentOne =
                (decimal)(((double)(arrearsInterestForRepresentmentOne + principle + interest + defaultCharge)) * percentageToBeCollectedForRepresentmentOne);

            var amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentOne, 2, MidpointRounding.AwayFromZero);

			Assert.AreEqual(amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces, transactionForRepresentmentOne.Amount);

            Do.Until(() => (_opsSagasMultipleRepresentmentsInArrearsSagaEntity.FindBy(ApplicationId: application.Id) == null));
        }

		[Test, AUT(AUT.Ca), JIRA("CA-1962"), FeatureSwitch(FeatureSwitchConstants.MultipleRepresentmentsInArrearsFeatureSwitchKey), Owner(Owner.TarasKudryavtsev)]
        public void WhenTheFirstPadRepresentmentForACustomerInArrearsIsSuccessfulThenASecondAttemptToRetrieve50PercentOfTheBalanceShouldBeMadeOnTheNextPayDate()
        {
            const double percentageToBeCollectedForRepresentmentOne = 0.33;
            const double percentageToBeCollectedForRepresentmentTwo = 0.50;
            const int loanTerm = 10;
            const decimal principle = 100;
            const decimal interest = 10;
            const decimal defaultCharge = 20;
            var customer = CustomerBuilder.New().WithIncomeFrequency(IncomeFrequencyEnum.BiWeekly).Build();
            var application = ApplicationBuilder.New(customer).WithLoanTerm(loanTerm).Build();

            var nextPayDateForRepresentmentOne = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(DateTime.Today, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                                        (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentOne = (int)nextPayDateForRepresentmentOne.Subtract(DateTime.Today).TotalDays;

            application.PutIntoArrears((uint)numOfDaysToNextPayDateForRepresentmentOne);
			TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentOne);

			var arrearsInterestForRepresentmentOne =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principle + interest), numOfDaysToNextPayDateForRepresentmentOne));

			WaitForProcessedMockedTransaction(customer.BankAccountNumber);
            TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForSuccessfulCashInTransactions(application.Id, 1);

            var amountToBeCollectedForRepresentmentOne =
                (decimal)(((double)(arrearsInterestForRepresentmentOne + principle + interest + defaultCharge)) * percentageToBeCollectedForRepresentmentOne);

            var amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentOne, 2, MidpointRounding.AwayFromZero);

            var currentDateForRepresentmentTwo = Convert.ToDateTime(customer.GetNextPayDate());

            var nextPayDateForRepresentmentTwo = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(currentDateForRepresentmentTwo, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                            (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentTwo = (int)nextPayDateForRepresentmentTwo.Subtract(currentDateForRepresentmentTwo).TotalDays;

            TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentTwo);
            application.RewindApplicationFurther((uint)numOfDaysToNextPayDateForRepresentmentTwo);

			WaitForProcessedMockedTransaction(customer.BankAccountNumber);
            TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForSuccessfulCashInTransactions(application.Id, 2);

            var transactionForRepresentmentTwo = _bgTrans.FindAll(_bgTrans.ApplicationId == application.Id &&
                                _bgTrans.TransactionStatus == (int)BankGatewayTransactionStatus.Paid).
                   OrderByTransactionIdDescending().First();

            var principleBalanceAfterRepresentmentOne = principle - amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces;

            var arrearsInterestForRepresentmentTwo =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principleBalanceAfterRepresentmentOne + interest), numOfDaysToNextPayDateForRepresentmentTwo));

            var amountToBeCollectedForRepresentmentTwo =
                (decimal)(((double)((arrearsInterestForRepresentmentOne + arrearsInterestForRepresentmentTwo + principleBalanceAfterRepresentmentOne + interest + defaultCharge))
                                                                                                                                    * percentageToBeCollectedForRepresentmentTwo));

            var amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentTwo, 2, MidpointRounding.AwayFromZero);

            Assert.AreEqual(amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces,transactionForRepresentmentTwo.Amount);
			Assert.AreEqual(amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces, CurrentRepresentmentAmount(application.Id));
            Assert.AreEqual(2, GetNumberOfRepresentmentsSent(application.Id));
            Assert.IsTrue(VerifyPaymentFunctions.VerifyDirectBankPaymentOfAmount(application.Id, -amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces));

        }

		[Test, AUT(AUT.Ca), JIRA("CA-1962"), FeatureSwitch(FeatureSwitchConstants.MultipleRepresentmentsInArrearsFeatureSwitchKey), Owner(Owner.TarasKudryavtsev)]
        public void WhenTheSecondPadRepresentmentForACustomerInArrearsFailsThenThereShouldBeNoMoreAttemptsToRetrieveMoneyFromTheCustomer()
        {
            const double percentageToBeCollectedForRepresentmentOne = 0.33;
            const double percentageToBeCollectedForRepresentmentTwo = 0.50;
            const int loanTerm = 10;
            const decimal principle = 100;
            const decimal interest = 10;
            const decimal defaultCharge = 20;
            var customer = CustomerBuilder.New().WithIncomeFrequency(IncomeFrequencyEnum.BiWeekly).Build();
            var application = ApplicationBuilder.New(customer).WithLoanTerm(loanTerm).Build();

            var nextPayDateForRepresentmentOne = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(DateTime.Today, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                                        (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentOne = (int)nextPayDateForRepresentmentOne.Subtract(DateTime.Today).TotalDays;

            application.PutIntoArrears((uint)numOfDaysToNextPayDateForRepresentmentOne);
			TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentOne);

            var arrearsInterestForRepresentmentOne =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principle + interest), numOfDaysToNextPayDateForRepresentmentOne));

			WaitForProcessedMockedTransaction(customer.BankAccountNumber);
			TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForSuccessfulCashInTransactions(application.Id, 1);

            var amountToBeCollectedForRepresentmentOne =
               (arrearsInterestForRepresentmentOne + principle + interest + defaultCharge) * (decimal)percentageToBeCollectedForRepresentmentOne;

            var amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentOne, 2, MidpointRounding.AwayFromZero);

            var currentDateForRepresentmentTwo = Convert.ToDateTime(customer.GetNextPayDate());

            var nextPayDateForRepresentmentTwo = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(currentDateForRepresentmentTwo, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                            (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentTwo = (int)nextPayDateForRepresentmentTwo.Subtract(currentDateForRepresentmentTwo).TotalDays;

            TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentTwo);
            application.RewindApplicationFurther((uint)numOfDaysToNextPayDateForRepresentmentTwo);

            ScotiaResponseBuilder.New().
                ForBankAccountNumber(customer.BankAccountNumber).
				Reject();

            TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForFailedCashInTransactions(application.Id, customer.BankAccountNumber, 2);

            Do.Until(() => (int)_bgTrans.GetCount(_bgTrans.ApplicationId == application.Id &&
                                       _bgTrans.TransactionStatus ==
                                       (int)BankGatewayTransactionStatus.Failed) == 2);

            var transactionForRepresentmentTwo = _bgTrans.FindAll(_bgTrans.ApplicationId == application.Id &&
                                _bgTrans.TransactionStatus == (int)BankGatewayTransactionStatus.Failed).
                   OrderByTransactionIdDescending().First();

            var principleBalanceAfterRepresentmentOne = principle - amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces;

            var arrearsInterestForRepresentmentTwo =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principleBalanceAfterRepresentmentOne + interest), numOfDaysToNextPayDateForRepresentmentTwo));

            var amountToBeCollectedForRepresentmentTwo =
                (arrearsInterestForRepresentmentOne + arrearsInterestForRepresentmentTwo + principleBalanceAfterRepresentmentOne + interest + defaultCharge)
                * (decimal)percentageToBeCollectedForRepresentmentTwo;

            var amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentTwo, 2, MidpointRounding.AwayFromZero);

            AssertAmountsAreSimilar(amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces, transactionForRepresentmentTwo.Amount);

			Do.Until(() => (_opsSagasMultipleRepresentmentsInArrearsSagaEntity.FindBy(ApplicationId: application.Id) == null));
        }

		[Test, AUT(AUT.Ca), JIRA("CA-1962"), FeatureSwitch(FeatureSwitchConstants.MultipleRepresentmentsInArrearsFeatureSwitchKey), Owner(Owner.TarasKudryavtsev)]
        public void WhenTheSecondPadRepresentmentForACustomerInArrearsIsSuccessfulThenAThirdAttemptToRetrieveTheRemainingBalanceShouldBeMadeOnTheNextPayDate()
        {
            const double percentageToBeCollectedForRepresentmentOne = 0.33;
            const double percentageToBeCollectedForRepresentmentTwo = 0.50;
            const int loanTerm = 10;
            const decimal principle = 100;
            const decimal interest = 10;
            const decimal defaultCharge = 20;
            var customer = CustomerBuilder.New().WithIncomeFrequency(IncomeFrequencyEnum.BiWeekly).Build();
            var application = ApplicationBuilder.New(customer).WithLoanTerm(loanTerm).Build();

            var nextPayDateForRepresentmentOne = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(DateTime.Today, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                                        (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentOne = (int)nextPayDateForRepresentmentOne.Subtract(DateTime.Today).TotalDays;

            application.PutIntoArrears((uint)numOfDaysToNextPayDateForRepresentmentOne);
			TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentOne);
			
            var arrearsInterestForRepresentmentOne =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principle + interest), numOfDaysToNextPayDateForRepresentmentOne, false));

			WaitForProcessedMockedTransaction(customer.BankAccountNumber);
			TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForSuccessfulCashInTransactions(application.Id, 1);

            var amountToBeCollectedForRepresentmentOne =
                (arrearsInterestForRepresentmentOne + principle + interest + defaultCharge) 
				* (decimal)percentageToBeCollectedForRepresentmentOne;

            var amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentOne, 2, MidpointRounding.AwayFromZero);

			var currentDateForRepresentmentTwo = Convert.ToDateTime(customer.GetNextPayDate());

            var nextPayDateForRepresentmentTwo = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(currentDateForRepresentmentTwo, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                            (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentTwo = (int)nextPayDateForRepresentmentTwo.Subtract(currentDateForRepresentmentTwo).TotalDays;

            TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentTwo);
            application.RewindApplicationFurther((uint)numOfDaysToNextPayDateForRepresentmentTwo);

			WaitForProcessedMockedTransaction(customer.BankAccountNumber);
            TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForSuccessfulCashInTransactions(application.Id, 2);

            var principleBalanceAfterRepresentmentOne = principle - amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces;

            var arrearsInterestForRepresentmentTwo =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principleBalanceAfterRepresentmentOne+interest), numOfDaysToNextPayDateForRepresentmentTwo, false));

            var amountToBeCollectedForRepresentmentTwo =
                (arrearsInterestForRepresentmentOne + arrearsInterestForRepresentmentTwo + principleBalanceAfterRepresentmentOne + interest + defaultCharge) 
                 * (decimal)percentageToBeCollectedForRepresentmentTwo;

            var amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentTwo, 2, MidpointRounding.AwayFromZero);

        	var currentDateForRepresentmentThree = Convert.ToDateTime(nextPayDateForRepresentmentTwo);

            var nextPayDateForRepresentmentThree = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(currentDateForRepresentmentThree, Convert.ToDateTime(nextPayDateForRepresentmentTwo),
                                                                (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentThree = (int)nextPayDateForRepresentmentThree.Subtract(currentDateForRepresentmentThree).TotalDays;

            TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentThree);
            application.RewindApplicationFurther((uint) numOfDaysToNextPayDateForRepresentmentThree);

			WaitForProcessedMockedTransaction(customer.BankAccountNumber);
            TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForSuccessfulCashInTransactions(application.Id, 3);

            var transactionForRepresentmentThree = _bgTrans.FindAll(_bgTrans.ApplicationId == application.Id &&
                                            _bgTrans.TransactionStatus == (int)BankGatewayTransactionStatus.Paid).OrderByTransactionIdDescending().First();

            var principleBalanceAfterRepresentmentTwo = principleBalanceAfterRepresentmentOne - amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces;

            var arrearsInterestForRepresentmentThree =
                CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principleBalanceAfterRepresentmentTwo + interest), numOfDaysToNextPayDateForRepresentmentThree, false);

			//last representment needs to take into account the round error of the previous ones
        	var amountToBeCollectedForRepresentmentThree =
        		arrearsInterestForRepresentmentOne + arrearsInterestForRepresentmentTwo + arrearsInterestForRepresentmentThree
        		+ principleBalanceAfterRepresentmentTwo + interest + defaultCharge;

            var amountToBeCollectedForRepresentmentThreeRoundedToTwoDecimalPlaces =
                        Decimal.Round(amountToBeCollectedForRepresentmentThree, 2, MidpointRounding.AwayFromZero);

            AssertAmountsAreSimilar(amountToBeCollectedForRepresentmentThreeRoundedToTwoDecimalPlaces, transactionForRepresentmentThree.Amount);
            Assert.IsTrue(VerifyPaymentFunctions.VerifyDirectBankPaymentOfAmount(application.Id, -amountToBeCollectedForRepresentmentThreeRoundedToTwoDecimalPlaces));
            Assert.IsTrue(Do.With.Timeout(1).Until(() => application.IsClosed));

			Do.Until(() => (_opsSagasMultipleRepresentmentsInArrearsSagaEntity.FindBy(ApplicationId: application.Id) == null));
        }

		[Test, AUT(AUT.Ca), JIRA("CA-1962"), FeatureSwitch(FeatureSwitchConstants.MultipleRepresentmentsInArrearsFeatureSwitchKey), Owner(Owner.TarasKudryavtsev)]
        public void WhenTheThirdPadRepresentmentForACustomerInArrearsFailsThenThereShouldBeNoMoreAttemptsToRetrieveMoneyFromTheCustomer()
        {
            const double percentageToBeCollectedForRepresentmentOne = 0.33;
            const double percentageToBeCollectedForRepresentmentTwo = 0.50;
            const int loanTerm = 10;
            const decimal principle = 100;
            const decimal interest = 10;
            const decimal defaultCharge = 20;
            var customer = CustomerBuilder.New().WithIncomeFrequency(IncomeFrequencyEnum.BiWeekly).Build();
            var application = ApplicationBuilder.New(customer).WithLoanTerm(loanTerm).Build();

            var nextPayDateForRepresentmentOne = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(DateTime.Today, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                                        (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentOne = (int)nextPayDateForRepresentmentOne.Subtract(DateTime.Today).TotalDays;

            application.PutIntoArrears((uint)numOfDaysToNextPayDateForRepresentmentOne);
			TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentOne);

            var arrearsInterestForRepresentmentOne =
                CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principle + interest), numOfDaysToNextPayDateForRepresentmentOne, false);

			WaitForProcessedMockedTransaction(customer.BankAccountNumber);
			TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForSuccessfulCashInTransactions(application.Id, 1);

            
			var amountToBeCollectedForRepresentmentOne =
                (arrearsInterestForRepresentmentOne + principle + interest + defaultCharge) * (decimal)percentageToBeCollectedForRepresentmentOne;

            var amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentOne, 2, MidpointRounding.AwayFromZero);

			var currentDateForRepresentmentTwo = Convert.ToDateTime(customer.GetNextPayDate());

            var nextPayDateForRepresentmentTwo = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(currentDateForRepresentmentTwo, Convert.ToDateTime(customer.GetNextPayDate()),
                                                                            (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentTwo = (int)nextPayDateForRepresentmentTwo.Subtract(currentDateForRepresentmentTwo).TotalDays;

            TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentTwo);
            application.RewindApplicationFurther((uint)numOfDaysToNextPayDateForRepresentmentTwo);
            TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForSuccessfulCashInTransactions(application.Id, 2);

            var principleBalanceAfterRepresentmentOne = principle - amountToBeCollectedForRepresentmentOneRoundedToTwoDecimalPlaces;

            var arrearsInterestForRepresentmentTwo =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principleBalanceAfterRepresentmentOne + interest), numOfDaysToNextPayDateForRepresentmentTwo, false));

            var amountToBeCollectedForRepresentmentTwo =
                (arrearsInterestForRepresentmentOne + arrearsInterestForRepresentmentTwo + principleBalanceAfterRepresentmentOne + interest + defaultCharge)
                 * (decimal)percentageToBeCollectedForRepresentmentTwo;

            var amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces =
                Decimal.Round(amountToBeCollectedForRepresentmentTwo, 2, MidpointRounding.AwayFromZero);

			var currentDateForRepresentmentThree = Convert.ToDateTime(nextPayDateForRepresentmentTwo);

            var nextPayDateForRepresentmentThree = CalculateNextPayDateFunctionsCa.CalculateNextPayDate(currentDateForRepresentmentThree, Convert.ToDateTime(nextPayDateForRepresentmentTwo),
                                                                (PaymentFrequency)(Convert.ToInt32(customer.GetIncomeFrequency())));
            var numOfDaysToNextPayDateForRepresentmentThree = (int)nextPayDateForRepresentmentThree.Subtract(currentDateForRepresentmentThree).TotalDays;

            TimeoutInArrearsNoticeSaga(application.Id, numOfDaysToNextPayDateForRepresentmentThree);
            application.RewindApplicationFurther((uint)numOfDaysToNextPayDateForRepresentmentThree);

            ScotiaResponseBuilder.New().
                ForBankAccountNumber(customer.BankAccountNumber).
				Reject();

            TimeoutMultipleRepresentmentsInArrearsSagaEntity(application.Id);
			WaitForFailedCashInTransactions(application.Id, customer.BankAccountNumber, 2);

            var transactionForRepresentmentThree = _bgTrans.FindAll(_bgTrans.ApplicationId == application.Id &&
                                            _bgTrans.TransactionStatus == (int)BankGatewayTransactionStatus.Failed).OrderByTransactionIdDescending().First();

            var principleBalanceAfterRepresentmentTwo = principleBalanceAfterRepresentmentOne - amountToBeCollectedForRepresentmentTwoRoundedToTwoDecimalPlaces;

            var arrearsInterestForRepresentmentThree =
                (CalculateFunctionsCa.CalculateExpectedArrearsInterestAmountAppliedCa(
                    (principleBalanceAfterRepresentmentTwo + interest), numOfDaysToNextPayDateForRepresentmentThree, false));
			
			//last representment needs to take into account the round error of the previous ones
        	var amountToBeCollectedForRepresentmentThree =
        		arrearsInterestForRepresentmentOne + arrearsInterestForRepresentmentTwo + arrearsInterestForRepresentmentThree
        		+ principleBalanceAfterRepresentmentTwo + interest + defaultCharge;

            var amountToBeCollectedForRepresentmentThreeRoundedToTwoDecimalPlaces =
                        Decimal.Round(amountToBeCollectedForRepresentmentThree, 2, MidpointRounding.AwayFromZero);

            AssertAmountsAreSimilar(amountToBeCollectedForRepresentmentThreeRoundedToTwoDecimalPlaces, transactionForRepresentmentThree.Amount);

			Do.Until(() => (_opsSagasMultipleRepresentmentsInArrearsSagaEntity.FindBy(ApplicationId: application.Id) == null));
        }

		[Test, AUT(AUT.Ca), JIRA("CA-1962"), FeatureSwitch(FeatureSwitchConstants.MultipleRepresentmentsInArrearsFeatureSwitchKey, true), ExpectedException(typeof(DoException)), Owner(Owner.TarasKudryavtsev)]
        public void WhenPadRepresentmentOptimizationFeatureSwitchIsOffThenTheMultipleRepresentmentsForPaymentsInArrearsSagaEntitySagaShouldNotBeUsed()
        {
            var customer = CustomerBuilder.New().Build();
            var application = ApplicationBuilder.New(customer).Build();

            application.PutIntoArrears();

            Do.Until(() => _opsSagasMultipleRepresentmentsInArrearsSagaEntity.FindByApplicationId(application.Id));
        }

		[Test, AUT(AUT.Ca), JIRA("CA-1962"), FeatureSwitch(FeatureSwitchConstants.MultipleRepresentmentsInArrearsFeatureSwitchKey, true), Owner(Owner.TarasKudryavtsev)]
        public void WhenPadRepresentmentOptimizationFeatureSwitchIsOffThenThePaymentsInArrearsSagaEntitySagaShouldBeUsed()
        {
            var customer = CustomerBuilder.New().Build();
            var application = ApplicationBuilder.New(customer).Build();

            application.PutIntoArrears();

            Do.Until(() => _opsSagasPaymentsInArrears.FindByApplicationId(application.Id));
        }

        private void TimeoutMultipleRepresentmentsInArrearsSagaEntity(Guid applicationGuid)
        {
            var multipleRepresentmentSaga = Do.Until(() => _opsSagasMultipleRepresentmentsInArrearsSagaEntity.FindByApplicationId(applicationGuid));
            Drive.Msmq.Payments.Send(new TimeoutMessage { SagaId = multipleRepresentmentSaga.Id });
        }

        private int GetNumberOfRepresentmentsSent(Guid applicationGuid)
        {
            var multipleRepresentmentSaga = Do.Until(() => _opsSagasMultipleRepresentmentsInArrearsSagaEntity.FindByApplicationId(applicationGuid));
            return multipleRepresentmentSaga.RepresentmentsSent;
        }

        private String GetNextRepresentmentDate(Guid applicationGuid)
        {
            var multipleRepresentmentSaga = Do.Until(() => _opsSagasMultipleRepresentmentsInArrearsSagaEntity.FindByApplicationId(applicationGuid));
            return multipleRepresentmentSaga.NextRepresentmentDate.ToString();
        }

        private Decimal CurrentRepresentmentAmount(Guid applicationGuid)
        {
            var multipleRepresentmentSaga = Do.Until(() => _opsSagasMultipleRepresentmentsInArrearsSagaEntity.FindByApplicationId(applicationGuid));
            return Math.Round(Convert.ToDecimal(multipleRepresentmentSaga.LastRepresentmentAmount), 2, MidpointRounding.AwayFromZero);
        }

        private void TimeoutInArrearsNoticeSaga(Guid applicationGuid, int numberOfDaysInArrears)
        {
            var inArrearsNoticeSaga =
                Do.Until(() => _inArrearsNoticeSaga.FindByApplicationId(applicationGuid));

            for (var i = 0; i < numberOfDaysInArrears; i++)
            {
                Drive.Msmq.Payments.Send(new TimeoutMessage { SagaId = inArrearsNoticeSaga.Id });
            }
        }

		private void AssertAmountsAreSimilar(decimal expectedAmount, decimal actualAmount, decimal allowedDifference = 0.01M)
		{
			Assert.LessThanOrEqualTo(Math.Abs(expectedAmount - actualAmount), allowedDifference);
		}

		private void WaitForProcessedMockedTransaction(string bankAccount)
		{
			Do.Until(() => _mockedBankGatewayResponses.FindBy(
				Gateway: "Scotia", BankAccount: bankAccount) == null);
		}

		private void WaitForFailedCashInTransactions(Guid applicationId, string bankAccount, int count)
		{
			Do.Until(() => (int)_bgTrans.GetCount(
				_bgTrans.ApplicationId == applicationId &&
				_bgTrans.TransactionStatus == (int)BankGatewayTransactionStatus.Failed &&
				_bgTrans.ServiceTypeID == (int)BankGatewayServiceTypeId.Collection) == count);

			WaitForProcessedMockedTransaction(bankAccount);
		}

		private void WaitForSuccessfulCashInTransactions(Guid applicationId, int count)
		{
			Do.Until(() => (int)_bgTrans.GetCount(
				_bgTrans.ApplicationId == applicationId &&
				_bgTrans.TransactionStatus == (int)BankGatewayTransactionStatus.Paid &&
				_bgTrans.ServiceTypeID == (int)BankGatewayServiceTypeId.Collection) == count);


			var application = _paymentApplications.FindBy(ExternalId: applicationId);

			Do.Until(() => (int)_paymentTransactions.GetCount(
				_paymentTransactions.ApplicationId == application.ApplicationId &&
				_paymentTransactions.Type == PaymentTransactionType.DirectBankPayment.ToString()) == count);
		}

    	

    }
}
