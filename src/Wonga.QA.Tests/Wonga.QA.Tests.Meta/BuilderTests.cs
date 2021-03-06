﻿using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Account;
using Wonga.QA.Framework.Builders;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Old;
using Wonga.QA.Tests.Core;
using ApplicationBuilder = Wonga.QA.Framework.ApplicationBuilder;

namespace Wonga.QA.Tests.Meta
{
    [TestFixture, Parallelizable(TestScope.All), DependsOn(typeof(ColdStartTests)), Category(TestCategories.CoreTest)]
    public class BuilderTests
    {
        private Customer _customer;
        private Organisation _organisation;
        private PayLaterAccount _payLaterAccount;
        private ConsumerAccount _consumerAccount;

        [Test, Owner(Owner.StanDesyatnikov)]
        public void CustomerBuilderTest()
        {
            Assert.DoesNotThrow(() => _customer = CustomerBuilder.New().Build());
        }

        [Test, DependsOn("CustomerBuilderTest"), DependsOn("OrganisationBuilderTest"), Owner(Owner.StanDesyatnikov)]
        public void ApplicationBuilderTest()
        {
            ApplicationBuilder builder = Config.AUT == AUT.Wb ?
                ApplicationBuilder.New(_customer, _organisation) :
                ApplicationBuilder.New(_customer);

            builder.Build();
        }

		[Test, AUT(AUT.Wb), DependsOn("CustomerBuilderTest"), Owner(Owner.AdrianMurphy)]
		public void OrganisationBuilderTest()
		{
			Assert.DoesNotThrow(() => _organisation = OrganisationBuilder.New(_customer).Build());
		}

        [Test, AUT(AUT.Uk), Owner(Owner.LukeRickard)]
        public void PayLaterAccountBuilderTest()
        {
            Assert.DoesNotThrow(() => _payLaterAccount = AccountBuilder.PayLater.New().Build());
        }

        [Test, AUT(AUT.Uk), DependsOn("PayLaterAccountBuilderTest"), Owner(Owner.LukeRickard)]
        public void PayLaterApplicationBuilderTests()
        {
            if (_payLaterAccount == null)
            {
                PayLaterAccountBuilderTest();
            }
            
            Assert.DoesNotThrow(() =>Framework.Builders.ApplicationBuilder.PayLater.New(_payLaterAccount).Build());
        }

        [Test, Owner(Owner.LukeRickard), Ignore()]
        public void ConsumerAccountBuilderTest()
        {
            Assert.DoesNotThrow(() =>_consumerAccount = AccountBuilder.Consumer.New().Build());
        }

        [Test, DependsOn("ConsumerAccountBuilderTest"), Owner(Owner.LukeRickard), Ignore()]
        public void PayLaterApplicationBuilderWithConsumerLoanAccount()
        {
            if (_consumerAccount == null)
            {
                ConsumerAccountBuilderTest();
            }

            Assert.DoesNotThrow(() =>Framework.Builders.ApplicationBuilder.PayLater.New(_consumerAccount).Build());
        }
    }
}
