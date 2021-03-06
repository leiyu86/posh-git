﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Api.Requests.Comms.Commands;
using Wonga.QA.Framework.Api.Requests.Comms.Commands.Uk;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Db.Comms;
using Wonga.QA.Tests.Core;
using Wonga.QA.Framework.Api.Exceptions;

namespace Wonga.QA.Tests.Comms
{
    [TestFixture]
    [Parallelizable(TestScope.All)]
    public class MobilePhoneTests
    {
        [Test,AUT(AUT.Wb)]
        [JIRA("SME-563"),Description("This test sends mobile phone verification message to predefined number")]
        public void TestVerifyMobilePhoneCommand()
        {
            var verificationId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            const string mobilePhone = "07200000000";

            Assert.DoesNotThrow(() => Drive.Api.Commands.Post(new VerifyMobilePhoneUkCommand()
                              {
                                  AccountId = accountId,
                                  Forename = Get.RandomString(8),
                                  MobilePhone = mobilePhone,
                                  VerificationId = verificationId
                              }));
            var mobileVerificationEntity = Do.Until(() => Drive.Db.Comms.MobilePhoneVerifications.SingleOrDefault(p => p.VerificationId == verificationId));
            Assert.IsNotNull(mobileVerificationEntity);
            Assert.AreEqual(mobileVerificationEntity.AccountId, accountId, "These values should be equal");
            Assert.AreEqual(mobileVerificationEntity.MobilePhone, mobilePhone, "These values should be equal");
        }

        [Test,AUT(AUT.Wb)]
        [JIRA("SME-563"),Description("This negative test attempts to send verification sms to invalid UK phone numbers and checks for expected failure")]
        public void TestVerifyMobilePhoneCommand_WithInvalidNumber()
        {
            const string invalidMobilePhone = "072000000000";
            var exceptionObject = Assert.Throws<ValidatorException>(() => Drive.Api.Commands.Post(new VerifyMobilePhoneUkCommand()
                                                                                         {
                                                                                             AccountId = Guid.NewGuid(),
                                                                                             Forename =
                                                                                                 Get.RandomString(8),
                                                                                             MobilePhone =
                                                                                                 invalidMobilePhone,
                                                                                             VerificationId =
                                                                                                 Guid.NewGuid()
                                                                                         }));

            Assert.AreEqual(exceptionObject.Errors.ToList()[0], "Ops_RequestXmlInvalid", "These values should be equal");
        }

        [Test,AUT(AUT.Wb)]
        [JIRA("SME-563"),Description("This test covers the process of creating a new customer details record, initiates mobile phone verification and completes it with correct PIN")]
        public void CompleteMobilePhoneVerification()
        {
            var accountId = Guid.NewGuid();
            var verificationId = Guid.NewGuid();
            string mobilePhoneNumber = Get.GetMobilePhone();
            var commsDb = Drive.Db.Comms;
            var newEntity = new CustomerDetailEntity
            {
                AccountId = accountId,
                Gender = 2,
                DateOfBirth = Get.GetDoB(),
                Email = Get.RandomEmail(),
                Forename = Get.RandomString(8),
                Surname = Get.RandomString(8),
                MiddleName = Get.RandomString(8),
                HomePhone = "0217050520",
                WorkPhone = "0217450510",
                MobilePhone = mobilePhoneNumber
            };

            commsDb.CustomerDetails.InsertOnSubmit(newEntity);
            commsDb.SubmitChanges();

            Assert.DoesNotThrow(() => Drive.Api.Commands.Post(new VerifyMobilePhoneUkCommand()
                                          {
                                              AccountId = accountId,
                                              Forename = Get.RandomString(8),
                                              MobilePhone = mobilePhoneNumber,
                                              VerificationId = verificationId
                                          }));
            var mobileVerificationEntity =
                Do.Until(() => Drive.Db.Comms.MobilePhoneVerifications.SingleOrDefault(p => p.VerificationId == verificationId));
            Assert.IsNotNull(mobileVerificationEntity);
            Assert.IsNotNull(mobileVerificationEntity.Pin);

            Assert.DoesNotThrow(() => Drive.Api.Commands.Post(new CompleteMobilePhoneVerificationCommand()
                                                                 {
                                                                     Pin = mobileVerificationEntity.Pin,
                                                                     VerificationId = verificationId
                                                                 }));
        }

        [Test,AUT(AUT.Wb)]
        [JIRA("SME-563"),Description("This negative test covers the process of creating a new customer details record, initiates mobile phone verification and completes it with incorrect PIN, while checking for failure")]
        public void TestCompleteEmailVerificationCommand_WithInvalidPin()
        {
            var exceptionObject = Assert.Throws<ValidatorException>(() => Drive.Api.Commands.Post(new CompleteMobilePhoneVerificationCommand()
                                                             {
                                                                 Pin = "",
                                                                 VerificationId = Guid.NewGuid()
                                                             }));

            Assert.AreEqual(exceptionObject.Errors.ToList()[0], "Ops_RequestXmlInvalid", "These values should be equal");
        }

        [Test,AUT(AUT.Wb)]
        [JIRA("SME-563"),Description("This test initiates the resend pin feature for new customer details record")]
        public void TestResendMobilePhonePin()
        {
            var accountId = Guid.NewGuid();
            var verificationId = Guid.NewGuid();
            string mobilePhoneNumber = Get.GetMobilePhone();
            var commsDb = Drive.Db.Comms;
            var newEntity = new CustomerDetailEntity
            {
                AccountId = accountId,
                Gender = 2,
                DateOfBirth = Get.GetDoB(),
                Email = Get.RandomEmail(),
                Forename = Get.RandomString(8),
                Surname = Get.RandomString(8),
                MiddleName = Get.RandomString(8),
                HomePhone = "0217050520",
                WorkPhone = "0217450510",
                MobilePhone = mobilePhoneNumber
            };

            commsDb.CustomerDetails.InsertOnSubmit(newEntity);
            commsDb.SubmitChanges();

            Assert.DoesNotThrow(() => Drive.Api.Commands.Post(new VerifyMobilePhoneUkCommand()
            {
                AccountId = accountId,
                Forename = Get.RandomString(8),
                MobilePhone = mobilePhoneNumber,
                VerificationId = verificationId
            }));
            var mobileVerificationEntity = Do.Until(() => Drive.Db.Comms.MobilePhoneVerifications.SingleOrDefault(p => p.VerificationId == verificationId));
            Assert.IsNotNull(mobileVerificationEntity);

            Assert.DoesNotThrow(()=>Drive.Api.Commands.Post(new ResendMobilePhonePinCommand()
                                                                 {
                                                                     Forename = Get.RandomString(8),
                                                                     VerificationId = mobileVerificationEntity.VerificationId
                                                                 }));
        }
    }
}
