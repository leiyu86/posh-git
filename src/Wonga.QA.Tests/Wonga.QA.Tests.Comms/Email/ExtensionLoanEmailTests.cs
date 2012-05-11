﻿using System;
using System.Linq;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Db.Payments;
using Wonga.QA.Framework.Msmq;
using Wonga.QA.Tests.Core;
using Wonga.QA.Tests.Payments.Helpers;
using AddPaymentCardCommand = Wonga.QA.Framework.Api.AddPaymentCardCommand;
using CreateFixedTermLoanExtensionCommand = Wonga.QA.Framework.Api.CreateFixedTermLoanExtensionCommand;

namespace Wonga.QA.Tests.Comms.Email
{

    [TestFixture]
    public class ExtensionLoanEmailTests
    {
        private LoanExtensionEntity _extension;

        public ExtensionLoanEmailTests()
        {
            _extension = CreateLoanAndExtend(); //run once for all tests.
        }
        
        [SetUp]
        public void Setup()
        {
            
        }

        [Test, AUT(AUT.Uk), JIRA("UK-1281"), Parallelizable]
        [Row(2, "Extension Secci")]
        [Row(3, "Extension Agreement")]
        [Row(20, "Extension AE Document")]
        public void CreateLoanExtensionDocumentsTest(int documentType)
        {
            Guid extensionId = _extension.ExternalId;

            dynamic documents = null;

            Assert.DoesNotThrow(() => documents = Do.Until(() =>
                            Drive.Data.Comms.Db.ExtensionDocuments.FindAllBy(ExtensionId: extensionId, DocumentType: documentType)
                            )
                , "ExtensionDocument not found for extension Id: {0} and docuemnt type:{1}", extensionId, documentType);


            Assert.AreEqual(1, documents.ToList().Count, "Document exist exactly once:{0}", documentType);

            Guid fileId = (Guid)documents.ToList()[0].ExternalId;
            Assert.DoesNotThrow(() => Do.Until(() => Drive.Data.FileStorage.Db.Files.FindByFileId(fileId))
                , "FileStorage record not found for FileId:{0}.", fileId);

        }

        [Test, AUT(AUT.Uk), JIRA("UK-1281"), Parallelizable]
        public void EmailExtensionAgreementTest()
        {

            Guid extensionId = _extension.ExternalId;

            var app = Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(x => x.ApplicationId == _extension.ApplicationId));

            Assert.DoesNotThrow(() => Do.Until(() => Drive.Data.Comms.Db.ExtensionDocuments.FindAllBy(ExtensionId: extensionId, DocumentType: 3))
                , "ExtensionDocument not found for extension Id: {0} and docuemnt type: Loan extension agreement", extensionId);


            Assert.DoesNotThrow(() =>
                    Do.Until(() => Drive.Data.OpsSagas.Db.EmailExtensionAgreementEntity.FindByExtensionId(extensionId))
                , "Email Extension Agreement Saga is NOT in progress: {0}", extensionId
            );

            Drive.Msmq.Comms.Send(new IExtensionSignedEvent
                                      {
                                          ApplicationId = app.ExternalId,
                                          ExtensionId = extensionId,
                                          CreatedOn = DateTime.UtcNow,
                                      });

            Assert.DoesNotThrow(() =>
                    Do.Until(() => (bool)(Drive.Data.OpsSagas.Db.EmailExtensionAgreementEntity.FindByExtensionId(extensionId) == null))
                , "Email Extension Agreement Saga has not completed: {0}", extensionId
            );

        }

        private LoanExtensionEntity CreateLoanAndExtend()
        {
            const decimal trustRating = 400.00M;
            var accountId = Guid.NewGuid();
            var bankAccountId = Guid.NewGuid();
            var paymentCardId = Guid.NewGuid();
            var appId = Guid.NewGuid();
            var extensionId = Guid.NewGuid();

            var setupData = new AccountSummarySetupFunctions();
            var clientId = Guid.NewGuid();

            CreateCommsData(clientId, accountId);

            setupData.Scenario03Setup(appId, paymentCardId, bankAccountId, accountId, trustRating);

            var app = Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(x => x.ExternalId == appId));
            var fixedTermApp =
                Do.With.Interval(1).Until(
                    () => Drive.Db.Payments.FixedTermLoanApplications.Single(x => x.ApplicationId == app.ApplicationId));

            Drive.Api.Commands.Post(new AddPaymentCardCommand
            {
                AccountId = accountId,
                PaymentCardId = paymentCardId,
                CardType = "VISA",
                Number = "4444333322221111",
                HolderName = "Test Holder",
                StartDate = DateTime.Today.AddYears(-1).ToDate(DateFormat.YearMonth),
                ExpiryDate = DateTime.Today.AddMonths(6).ToDate(DateFormat.YearMonth),
                IssueNo = "000",
                SecurityCode = "666",
                IsCreditCard = false,
                IsPrimary = true,
            });

            Do.With.Interval(1).Until(
                () => Drive.Db.Payments.PaymentCardsBases.Single(x => x.ExternalId == paymentCardId && x.AuthorizedOn != null));

            Drive.Api.Commands.Post(new CreateFixedTermLoanExtensionCommand
            {
                ApplicationId = appId,
                ExtendDate = new Date(fixedTermApp.NextDueDate.Value.AddDays(2), DateFormat.Date),
                ExtensionId = extensionId,
                PartPaymentAmount = 20M,
                PaymentCardCv2 = "000",
                PaymentCardId = paymentCardId
            });

            var loanExtension =
                Do.With.Interval(1).Until(
                    () =>
                    Drive.Db.Payments.LoanExtensions.Single(x => x.ExternalId == extensionId && x.ApplicationId == app.ApplicationId
                        && x.PartPaymentTakenOn != null));

            Assert.IsNotNull(loanExtension, "A loan extension should be created");

            return loanExtension;
        }

        private static void CreateCommsData(Guid clientId, Guid accountId)
        {
            Drive.Msmq.Comms.Send(new
                                      SaveCustomerDetailsCommand
            {
                AccountId = accountId,
                ClientId = clientId,
                CreatedOn = DateTime.UtcNow,
                DateOfBirth = new DateTime(1956, 10, 17),
                Email = string.Format("mdwonga+{0}@gmail.com", DateTime.UtcNow.Ticks),
                Forename = string.Format("Joe_{0}", DateTime.UtcNow.Ticks),
                Gender = GenderEnum.Male,
                HomePhone = string.Format("02{0}", DateTime.UtcNow.Ticks.ToString().Substring(0, 8)),
                MiddleName = "X",
                MobilePhone = string.Format("07{0}", DateTime.UtcNow.Ticks.ToString().Substring(0, 8)),
                Surname = string.Format("Doe{0}", DateTime.UtcNow.Ticks),
                Title = TitleEnum.Dr,
                WorkPhone = "02078889999"
            });
            Drive.Msmq.Comms.Send(new IAccountCreatedEvent { AccountId = accountId });


            Drive.Msmq.Comms.Send(new
                                      SaveCustomerAddressCommand
            {
                CreatedOn = DateTime.UtcNow,
                AccountId = accountId,
                AddressId = Guid.NewGuid(),
                AtAddressFrom = new DateTime(2000, 1, 1),
                ClientId = clientId,
                CountryCode = CountryCodeEnum.Uk,
                Flat = "22",
                HouseName = "7",
                Postcode = "W7 3BX",
                Town = "London",
                Street = "Church Road",
                District = "East",
            });
        }
    }
}