﻿using System;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.Sms;
using Wonga.QA.Framework.Old;
using Wonga.QA.Tests.Core;
using Wonga.QA.Framework.Data.Enums.Sms;

namespace Wonga.QA.Tests.Sms
{
    public class MbloxIntegrationTests
    {

        private const string TEST_PHONE_NUMBER = "447786777486";
        private const string FAILED_PHONE_NUMBER = "000000000000";
        private const string MBLOX_MESSAGE_TEXT_TEST = "Sending test message from acceptance tests with using Mblox provider with GUID : ";
        private const string ZONG_MESSAGE_TEXT_TEST = "Sending test message from acceptance tests with using Zong provider with GUID :";
        private const string PROVIDER_CHOOSE_KEY = "Sms.Uk_Provider_name";
        private const string ZONG_PROVIDER_NAME = "Zong";
        private const string MBLOX_PROVIDER_NAME = "Mblox";
        private const string SMS_PROVIDER_RESPONSE = "SMS Mock says OK!";

        private static readonly dynamic _smsEntity = Drive.Data.Sms.Db.SmsMessages;

        private Customer _customer = null;

        [SetUp]
        public void Init()
        {
            _customer = CustomerBuilder.New().Build();
        }

        [Test, JIRA("UK-510"), AUT(AUT.Uk), Owner(Owner.SvyatoslavKravchenko),Explicit]
        public void SendRequestToMbloxProvider()
        {
            if (Drive.Data.Ops.GetServiceConfiguration<String>(PROVIDER_CHOOSE_KEY).Equals(ZONG_PROVIDER_NAME))
                Drive.Data.Ops.SetServiceConfiguration(PROVIDER_CHOOSE_KEY, MBLOX_PROVIDER_NAME);

            var request = new SendSmsMessage()
            {

                AccountId = _customer.Id,
                MessageText = MBLOX_MESSAGE_TEXT_TEST + "" + Guid.NewGuid(),
                OriginatingSagaId = Guid.NewGuid(),
                ToNumberFormatted = FAILED_PHONE_NUMBER
            };

            Drive.Msmq.SmsDistrubutor.Send(request);
            var sendMessage = Do.Until(() => _smsEntity.FindBy(MessageText: request.MessageText));


            Assert.IsTrue(sendMessage.MobilePhoneNumber.Equals(request.ToNumberFormatted));
            Assert.IsTrue(sendMessage.MessageText.Equals(request.MessageText));
            Assert.IsTrue(((int)UKSmsStatuses.New).Equals(sendMessage.Status));
            Assert.IsNull(sendMessage.ErrorMessage);

            Assert.IsTrue(Drive.Data.Ops.GetServiceConfiguration<String>(PROVIDER_CHOOSE_KEY).Equals(MBLOX_PROVIDER_NAME));
        }

        [Test, AUT(AUT.Uk), JIRA("UK-510"), Owner(Owner.SvyatoslavKravchenko),Explicit]
        public void SwitchToAnotherProviderAndSendSmsRequest()
        {
            Do.Until(() => Drive.Data.Ops.SetServiceConfiguration(PROVIDER_CHOOSE_KEY, ZONG_PROVIDER_NAME));

            var request = new SendSmsMessage()
            {
                AccountId = _customer.Id,
                MessageText = ZONG_MESSAGE_TEXT_TEST + "" + Guid.NewGuid(),
                OriginatingSagaId = Guid.NewGuid(),
                ToNumberFormatted = TEST_PHONE_NUMBER
            };

            Drive.Msmq.SmsDistrubutor.Send(request);
            var message = Do.Until(() => _smsEntity.FindBy(MessageText: request.MessageText));

            Assert.IsTrue(message.MobilePhoneNumber.Equals(request.ToNumberFormatted));
            Assert.IsTrue(message.MessageText.Equals(request.MessageText));
            Assert.IsTrue(SMS_PROVIDER_RESPONSE.Equals(message.ErrorMessage));
            Assert.IsTrue(((int)UKSmsStatuses.Delivered).Equals(message.Status));

            Assert.IsTrue(Drive.Data.Ops.GetServiceConfiguration<String>(PROVIDER_CHOOSE_KEY).Equals(ZONG_PROVIDER_NAME));
        }

        [TearDown]
        public void Rollback()
        {
            Do.Until(() => Drive.Data.Ops.SetServiceConfiguration(PROVIDER_CHOOSE_KEY, MBLOX_PROVIDER_NAME));
        }
    }
}