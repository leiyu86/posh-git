﻿using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.Salesforce
{
	[TestFixture]
	public class SalesforcePushGuarantorDetailsTests : SalesforceTestBase
	{
		[Test, AUT(AUT.Wb), JIRA("SME-375")]
		public void ShouldUpdateSalesforceContact_WhenIndividualGuarantorSignsApplicationTerms()
		{
			const int expectedStatus = 104;

			var customer = CustomerBuilder.New().Build();
			var organisation = OrganisationBuilder.New(customer).WithSoManySecondaryDirectors(3).Build();
			ApplicationBuilder.New(customer, organisation).Build();

			Do.Until(() => Salesforce.GetContactByStatus(customer.Id, expectedStatus));
		}

		[Test, AUT(AUT.Wb), JIRA("SME-375")]
		public void ShouldUpdateSalesforceContact_WhenMainApplicantIsAccepted()
		{
			const int expectedStatus = 105;

			var customer = CustomerBuilder.New().Build();
			var organisation = OrganisationBuilder.New(customer).WithSoManySecondaryDirectors(3).Build();
			ApplicationBuilder.New(customer, organisation).Build();

			Do.Until(() => Salesforce.GetContactByStatus(customer.Id, expectedStatus));
		}
	}
}