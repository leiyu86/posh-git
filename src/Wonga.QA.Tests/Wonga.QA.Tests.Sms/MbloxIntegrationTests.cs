﻿using System;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.Sms;
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

        private static readonly dynamic _serviceConfigurationsEntity = Drive.Data.Ops.Db.ServiceConfigurations;
        private static readonly dynamic _smsEntity = Drive.Data.Sms.Db.SmsMessages;

        private Customer _customer = null;

        [SetUp]
        public void Init()
        {
            _customer = CustomerBuilder.New().Build();
        }
        [Test,  JIRA("UK-510")]
        public void SendRequestToMbloxProvider()
        {

            var request = new SendSmsMessage()
               {
                                 
                AccountId = _customer.Id,
                MessageText = MBLOX_MESSAGE_TEXT_TEST + "" + Guid.NewGuid(),
                OriginatingSagaId = Guid.NewGuid(),
                ToNumberFormatted = Get.GetMobilePhone()
            };

            var invalidRequestWithMobPhone = new SendSmsMessage()
            {
                AccountId = _customer.Id,
                MessageText = MBLOX_MESSAGE_TEXT_TEST + "" + Guid.NewGuid(),
                OriginatingSagaId = Guid.NewGuid(),
                ToNumberFormatted = FAILED_PHONE_NUMBER
            };

            Drive.Msmq.SmsDistrubutor.Send(request);
            Drive.Msmq.SmsDistrubutor.Send(invalidRequestWithMobPhone);

            var sendMessage = Do.Until(() => _smsEntity.FindBy(MessageText: request.MessageText));
            var failedMessage = Do.Until(() => _smsEntity.FindBy(MessageText: invalidRequestWithMobPhone.MessageText));

            Assert.IsTrue(sendMessage.MobilePhoneNumber.Equals(request.ToNumberFormatted));
            Assert.IsTrue(sendMessage.MessageText.Equals(request.MessageText));
            Assert.IsNull(sendMessage.ErrorMessage);
            Assert.IsTrue(((int)UKSmsStatuses.Delivered).Equals(sendMessage.Status));

            Assert.IsTrue(failedMessage.MobilePhoneNumber.Equals(invalidRequestWithMobPhone.ToNumberFormatted));
            Assert.IsTrue(failedMessage.MessageText.Equals(invalidRequestWithMobPhone.MessageText));
            Assert.IsNotNull(failedMessage.ErrorMessage);
            Assert.IsTrue(((int)UKSmsStatuses.Failed).Equals(failedMessage.Status));

            Assert.IsTrue(sendMessage.MobilePhoneNumber.Equals(request.ToNumberFormatted));
            Assert.IsTrue(sendMessage.MessageText.Equals(request.MessageText));
            Assert.IsNull(sendMessage.ErrorMessage);
            Do.Until(() => _serviceConfigurationsEntity.FindBy(Key: PROVIDER_CHOOSE_KEY, Value: MBLOX_PROVIDER_NAME));

        }

        [Test, AUT(AUT.Uk), JIRA("UK-510"),]
        public void SwitchToAnotherProviderAndSendSmsRequest()
        {
            changeProvider();

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
            Assert.IsNull(message.ErrorMessage);
            Do.Until(() => _serviceConfigurationsEntity.FindBy(Key: PROVIDER_CHOOSE_KEY, Value: ZONG_PROVIDER_NAME));

            changeProvider();
        }


        private void changeProvider()
        {
            var provider_name = Do.Until(() => _serviceConfigurationsEntity.FindBy(Key: PROVIDER_CHOOSE_KEY));
            var providerName = provider_name.Value;
            if (providerName.Equals(ZONG_PROVIDER_NAME))
            {
                Do.Until(() => _serviceConfigurationsEntity.UpdateByServiceConfigurationId(
                     ServiceConfigurationId: provider_name.ServiceConfigurationId,
                     Key: PROVIDER_CHOOSE_KEY, Value: MBLOX_PROVIDER_NAME));
            }

            if (providerName.Equals(MBLOX_PROVIDER_NAME))
            {
                Do.Until(() => _serviceConfigurationsEntity.UpdateByServiceConfigurationId(
                     ServiceConfigurationId: provider_name.ServiceConfigurationId,
                     Key: PROVIDER_CHOOSE_KEY, Value: ZONG_PROVIDER_NAME));
            }
        }
    }
}
