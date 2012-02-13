﻿using System;
using Wonga.QA.Framework.Api;



namespace Wonga.QA.Framework
{
    public class CompanyBuilder
    {
        private Guid _id;
        private Customer _customer;
        
        private CompanyBuilder()
        {
            _id = Guid.NewGuid();
        }

        public static CompanyBuilder New(Customer customer)
        {
            return new CompanyBuilder {_customer = customer};
        }

        public Company Build()
        {
            Driver.Api.Commands.Post(new ApiRequest[]
                                           {
                                               SaveOrganisationDetailsCommand.New(r=>r.OrganisationId = _id),
                                               AddBusinessBankAccountWbUkCommand.New(r=>r.OrganisationId = _id),
                                               AddBusinessPaymentCardWbUkCommand.New(r=>r.OrganisationId = _id),
                                               AddPrimaryOrganisationDirectorCommand.New(r=> { r.OrganisationId = _id; r.AccountId = _customer.Id; }),
                                               //AddSecondaryOrganisationDirectorCommand.New(r=>r.OrganisationId = _id)
                                           });
            return new Company(_id);
        }
    }
}
