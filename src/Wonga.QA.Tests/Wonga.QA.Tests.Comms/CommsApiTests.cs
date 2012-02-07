﻿using System;
using System.Linq;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Core;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.Comms
{
    public class CommsApiTests
    {
        [Test]
        public void GetCustomerDetails()
        {
            Driver.Api.Queries.Post(new GetCustomerDetailsQuery
            {
                AccountId = Driver.Db.Comms.CustomerDetails.First().AccountId
            });
        }

        [Test, AUT(AUT.Uk, AUT.Za, AUT.Ca)]
        public void SaveAndGetCustomerDetails()
        {
            ApiDriver api = new ApiDriver();
            Guid id = Guid.NewGuid();

            ApiRequest command;
            switch (Config.AUT)
            {
                /*case AUT.Uk:
                    command = SaveCustomerDetailsUkCommand.Random(r => r.AccountId = id);
                    break;*/
                case AUT.Za:
                    command = SaveCustomerDetailsZaCommand.New(r =>
                    {
                        r.AccountId = id;
                        r.NationalNumber = Data.GetNIN((Date)r.DateOfBirth, (GenderEnum)r.Gender == GenderEnum.Female);
                        r.MaidenName = (GenderEnum)r.Gender == GenderEnum.Female ? r.MaidenName : null;
                    });
                    break;
                /*case AUT.Ca:
                    command = SaveCustomerDetailsCaCommand.Random(r => r.AccountId = id);
                    break;*/
                default:
                    throw new NotImplementedException();
            }

            api.Commands.Post(command);
            api.Queries.Post(new GetCustomerDetailsQuery() { AccountId = id });
        }
    }
}
