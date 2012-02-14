﻿using System;
using System.Linq;
using Wonga.QA.Framework.Core;

namespace Wonga.QA.Framework
{
    public class Organisation
    {
        public Guid Id { get; set; }

        public Organisation(Guid id)
        {
            Id = id;
        }

        public Guid GetPaymentCard()
        {
            var paymentCardId = Do.Until(()=>Driver.Db.Payments.BusinessPaymentCards.Single(b => b.OrganisationId == Id).PaymentCardId);
            return Driver.Db.Payments.PaymentCardsBases.Single(a=>a.PaymentCardId == paymentCardId).ExternalId;
        }

        public Guid GetBankAccount()
        {
            var bankAccountId =Do.Until(()=>Driver.Db.Payments.BusinessBankAccounts.Single(b => b.OrganisationId == Id).BankAccountId);
            return Driver.Db.Payments.BankAccountsBases.Single(a=>a.BankAccountId == bankAccountId).ExternalId;
        }
    }
}