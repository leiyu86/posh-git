﻿using System;
using System.Data.Linq;
using System.Linq;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Db;
using Wonga.QA.Framework.Db.BankGateway;
using Wonga.QA.Framework.Msmq;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.BankGateway
{
    [TestFixture, AUT(AUT.Uk), Parallelizable(TestScope.Self)]
    public class HsbcSortCodesTests
    {
        private PaymentTypeEntity _type;
        private SortCodeEntity _code;
        private Int32 _rows;

        [FixtureSetUp]
        public void InsertTestPaymentType()
        {
            Table<PaymentTypeEntity> types = Driver.Db.BankGateway.PaymentTypes;
            _type = types.SingleOrDefault(t => t.Name == "QAF") ?? types.Insert(new PaymentTypeEntity { PaymentTypeId = types.Max(t => t.PaymentTypeId) + 1, Name = "QAF" }).Submit();
        }

        [FixtureTearDown]
        public void DeleteTestPaymentType()
        {
            _type.Delete().Submit();
        }

        [SetUp]
        public void ResetCreationDatesAndGetLastSortCode()
        {
            BankGatewayDatabase database = Driver.Db.BankGateway;
            _rows = database.ExecuteCommand(String.Format("UPDATE {0} SET CreationDate = {{0}}", database.SortCodes.GetName()), Get.GetDateTimeMin());
            _code = database.SortCodes.OrderByDescending(c => c.CreationDate).ThenByDescending(c => c.SortCodeId).First();
        }

        [Test, JIRA("UK-494")]
        [Ignore]//todo: Test needs updating
        public void SortCodesTableIsUpdatedOnRestart()
        {
            _code.Delete().Submit();

            Driver.Svc.Hsbc.Restart();
            Do.With().Timeout(3).Interval(10).Until(() => Driver.Db.BankGateway.SortCodes.Max(c => c.CreationDate) > Get.GetDateTimeMin());

            Table<SortCodeEntity> codes = Driver.Db.BankGateway.SortCodes;
            Assert.GreaterThanOrEqualTo(codes.Count(), _rows);
            Assert.Contains(codes.Select(c => c.SortCode), _code.SortCode);
        }

        [Test, JIRA("UK-494"), TestsOn(typeof(UpdateSortCodeTableUkCommand))]
        [Ignore] //todo: Test needs updating
        public void SortCodesTableIsUpdatedOnMessage()
        {
            _code.PaymentTypeId = _type.PaymentTypeId;
            _code.Submit();

            Driver.Msmq.Hsbc.Send(new UpdateSortCodeTableUkCommand());
            Do.With().Timeout(3).Interval(10).Until(() => Driver.Db.BankGateway.SortCodes.Max(c => c.CreationDate) > Get.GetDateTimeMin());

            Assert.GreaterThan(_code.Refresh().CreationDate, Get.GetDateTimeMin());
            Assert.AreNotEqual(_code.PaymentTypeId, _type.PaymentTypeId);
        }
    }
}
