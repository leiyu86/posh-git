﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Api.Exceptions;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Msmq;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.Payments
{
    [TestFixture]
    public class CsapiCreateTransactionTest
    {
        [Test, AUT(AUT.Wb), JIRA("SME-375")]
        public void PaymentsShouldInsertTransactionWhenValidRequestIsSubmitted()
        {
            var customer = CustomerBuilder.New().Build();
            var organisation = OrganisationBuilder.New(customer).Build();
            var app = ApplicationBuilder.New(customer, organisation).WithExpectedDecision(ApplicationDecisionStatusEnum.Accepted).Build();

            var createTransactionCommand = new Wonga.QA.Framework.Cs.CreateTransactionCommand
                                               {
                                                   Amount = 500.50m,
                                                   ApplicationGuid = app.Id,
                                                   Currency = CurrencyCodeEnum.GBP,
                                                   Reference = String.Empty,
                                                   SalesForceUser = "test.user@wonga.com",
                                                   Scope = PaymentTransactionScopeEnum.Credit,
                                                   Type = PaymentTransactionEnum.Cheque
                                               };
            Driver.Cs.Commands.Post(createTransactionCommand);

            Do.Until(() => Driver.Db.Payments.Transactions.Single(t => t.Amount == 500.50m
                                                                       && t.ApplicationEntity.ExternalId == app.Id
                                                                       && t.Reference == ""
                                                                       && t.SalesForceUsername == "test.user@wonga.com"
                                                                       && t.Scope == (int)PaymentTransactionScopeEnum.Credit
                                                                       && t.Type == PaymentTransactionEnum.Cheque.ToString()));
        }

        [Test, AUT(AUT.Wb), JIRA("SME-375")]
        public void PaymentsShouldReturnSchemaErrorAndNotInsertTransactionsWhenSalesforceUserIsNull()
        {
            var customer = CustomerBuilder.New().Build();
            var organisation = OrganisationBuilder.New(customer).Build();
            var app = ApplicationBuilder.New(customer, organisation).WithExpectedDecision(ApplicationDecisionStatusEnum.Accepted).Build();

            var createTransactionCommand = new Wonga.QA.Framework.Cs.CreateTransactionCommand
            {
                Amount = 500.50m,
                ApplicationGuid = app.Id,
                Currency = CurrencyCodeEnum.GBP,
                Reference = String.Empty,
                Scope = PaymentTransactionScopeEnum.Credit,
                Type = PaymentTransactionEnum.Cheque
            };
            Assert.Throws<SchemaException>(() => Driver.Cs.Commands.Post(createTransactionCommand));
            Assert.AreEqual(0,Driver.Db.Payments.Transactions.Count(t => t.Amount == 500.50m
                                                                       && t.ApplicationEntity.ExternalId == app.Id
                                                                       && t.Reference == ""
                                                                       && t.Scope == (int)PaymentTransactionScopeEnum.Credit
                                                                       && t.Type == PaymentTransactionEnum.Cheque.ToString()));
        }

        [Test, AUT(AUT.Wb), JIRA("SME-375")]
        public void PaymentsShouldReturnErrorAndNotInsertTransactionsWhenSalesforceUserIsEmpty()
        {
            var customer = CustomerBuilder.New().Build();
            var organisation = OrganisationBuilder.New(customer).Build();
            var app = ApplicationBuilder.New(customer, organisation).WithExpectedDecision(ApplicationDecisionStatusEnum.Accepted).Build();

            var createTransactionCommand = new Wonga.QA.Framework.Cs.CreateTransactionCommand
            {
                Amount = 500.50m,
                ApplicationGuid = app.Id,
                Currency = CurrencyCodeEnum.GBP,
                Reference = String.Empty,
                SalesForceUser = String.Empty,
                Scope = PaymentTransactionScopeEnum.Credit,
                Type = PaymentTransactionEnum.Cheque
            };
            var exception = Assert.Throws <ValidatorException>(() => Driver.Cs.Commands.Post(createTransactionCommand));
            Assert.Contains(exception.Errors, "Payments_SFUserId_NotSupplied");
           Assert.AreEqual(0,Driver.Db.Payments.Transactions.Count(t => t.Amount == 500.50m
                                                                       && t.ApplicationEntity.ExternalId == app.Id
                                                                       && t.Reference == ""
                                                                       && t.Scope == (int)PaymentTransactionScopeEnum.Credit
                                                                       && t.Type == PaymentTransactionEnum.Cheque.ToString()));
        }

        [Test, AUT(AUT.Wb), JIRA("SME-375")]
        [Row(PaymentTransactionEnum.Cheque, PaymentTransactionScopeEnum.Debit)]
        [Row(PaymentTransactionEnum.Adjustment, PaymentTransactionScopeEnum.Debit)]
        public void PaymentsShouldReturnErrorAndNotInsertTransactionsWhenTypeOrScopeIsInvalid(PaymentTransactionEnum type, PaymentTransactionScopeEnum scope)
        {
            var customer = CustomerBuilder.New().Build();
            var organisation = OrganisationBuilder.New(customer).Build();
            var app = ApplicationBuilder.New(customer, organisation).WithExpectedDecision(ApplicationDecisionStatusEnum.Accepted).Build();

            var createTransactionCommand = new Wonga.QA.Framework.Cs.CreateTransactionCommand
            {
                Amount = 500.50m,
                ApplicationGuid = app.Id,
                Currency = CurrencyCodeEnum.GBP,
                Reference = "",
                SalesForceUser = "test.user@wonga.com",
                Scope = scope,
                Type = type
            };
            var exc = Assert.Throws<ValidatorException>(() => Driver.Cs.Commands.Post(createTransactionCommand));
            Assert.Contains(exc.Errors, "Payments_TransactionType_Invalid");
            Assert.AreEqual(0, Driver.Db.Payments.Transactions.Count(t => t.Amount == 500.50m
                                                                        && t.ApplicationEntity.ExternalId == app.Id
                                                                        && t.Reference == ""
                                                                        && t.Scope == (int)PaymentTransactionScopeEnum.Credit
                                                                        && t.Type == PaymentTransactionEnum.Cheque.ToString()));
        }
    }
}