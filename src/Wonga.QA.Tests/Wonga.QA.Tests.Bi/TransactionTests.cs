﻿using System.Linq;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Db.Payments;
using Wonga.QA.Framework.Msmq;
using Wonga.QA.Framework.Old;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.Bi
{
	[Parallelizable(TestScope.All)]
	class TransactionTests
	{
		[Test, AUT(AUT.Za), Pending("Until BI works")]
		public void TransactionStoredInTableCashAdvance()
		{
			var typeName = "CashAdvance";

			var customer = CustomerBuilder.New().Build();
			var application = ApplicationBuilder.New(customer).Build();
			var transaction = GetTransactionFromPayments(application, typeName);
			WaitForTransactionExistsInBi(transaction);
		}

		[Test, AUT(AUT.Za), Pending("Until BI works")]
		public void TransactionStoredInTableInitiationFee()
		{
			var typeName = "InitiationFee";

			var customer = CustomerBuilder.New().Build();
			var application = ApplicationBuilder.New(customer).Build();
			var transaction = GetTransactionFromPayments(application, typeName);
			WaitForTransactionExistsInBi(transaction);
		}

		[Test, AUT(AUT.Za), Pending("Until BI works")]
		public void TransactionStoredInTable_ServiceFee()
		{
			var typeName = "ServiceFee";

			var customer = CustomerBuilder.New().Build();
			var application = ApplicationBuilder.New(customer).Build();
			var transaction = GetTransactionFromPayments(application, typeName);
			WaitForTransactionExistsInBi(transaction);
		}

		[Test, AUT(AUT.Za), Pending("Until BI works")]
		public void TransactionStoredInTableSuspendInterestAccrual()
		{
			var typeName = "SuspendInterestAccrual";

			var customer = CustomerBuilder.New().Build();
			var application = ApplicationBuilder.New(customer).Build();
			var transaction = GetTransactionFromPayments(application, typeName);
			WaitForTransactionExistsInBi(transaction);
		}

		[Test, AUT(AUT.Za), Pending("Until BI works")]
		public void TransactionStoredInTableResumeInterestAccrual()
		{
			var typeName = "ResumeInterestAccrual";

			var customer = CustomerBuilder.New().Build();
			var application = ApplicationBuilder.New(customer).Build();
			var transaction = GetTransactionFromPayments(application, typeName);
			WaitForTransactionExistsInBi(transaction);
		}

		[Test, AUT(AUT.Za), Pending("Until BI works")]
		public void TransactionStoredInTableDirectBankPayment()
		{
			var typeName = "DirectBankPayment";
			var customer = CustomerBuilder.New().Build();
			var application = ApplicationBuilder.New(customer).Build().RepayOnDueDate();
			var transaction = GetTransactionFromPayments(application, typeName);
			WaitForTransactionExistsInBi(transaction);
		}

		#region Helpers

		private TransactionEntity GetTransactionFromPayments(Application application, string type)
		{
			Do.Until(
				() =>
				Drive.Db.Payments.Applications.Single(a => a.ExternalId == application.Id).Transactions.Any(
					t => t.Type == type));

			return Drive.Db.Payments.Applications.Single(a => a.ExternalId == application.Id).Transactions.Last(t => t.Type == type);
		}

		private void WaitForTransactionExistsInBi(TransactionEntity transaction)
		{
			Do.Until(() => Drive.Db.Bi.Transactions.Single(t => t.TransactionNKey == transaction.ExternalId));
		}

		#endregion
	}
}
