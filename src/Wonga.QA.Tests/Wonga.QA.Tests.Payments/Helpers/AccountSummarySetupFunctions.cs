﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Db.Payments;
using Wonga.QA.Framework.Msmq;

namespace Wonga.QA.Tests.Payments.Helpers
{
    public class AccountSummarySetupFunctions
    {
        private void CheckExtensionIsEnabled()
        {
            // Check Loan Extension is Enabled
            var cfg1 = Drive.Db.Ops.ServiceConfigurations.Single(a => a.Key == "Payments.ExtendLoanEnabled");
            if (cfg1.Value != "true")
                throw new Exception("Unable to run test, LoanExtension must be enabled in service configuration");
        }

        private void MakeAppNotTooNearDueDateToExtend(Guid appId)
        {
            var cfg = Drive.Data.Ops.Db.ServiceConfigurations.FindByKey("Payments.ExtendLoanDaysBeforeDueDate");
            UpdateNextDueDate(appId, int.Parse(cfg.Value) - 3);
        }

        private void MakeAppDueToday(Guid appId)
        {
            UpdateNextDueDate(appId, 0);
        }

        private void MakeAppDueYesterday(Guid appId)
        {
            UpdateNextDueDate(appId, -1);
        }

        private void MakeAppAcceptedOnNotTooEarlyToExtendDays(Guid appId)
        {
            // Check extend loan min days
            var cfg1 = Drive.Data.Ops.Db.ServiceConfigurations.FindByKey("Payments.ExtendLoanMinDays");
            try
            {
                UpdateAcceptedOn(appId, int.Parse(cfg1.Value + 5));
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
                UpdateAcceptedOn(appId, int.Parse(cfg1.Value + 5));
            }
        }

        private void MakeAppTooEarlyToExtendDays(Guid appId)
        {
            // Check extend loan min days
            var cfg1 = Drive.Data.Ops.Db.ServiceConfigurations.FindByKey("Payments.ExtendLoanMinDays");
            UpdateAcceptedOn(appId, 0);
        }

        private void UpdateAcceptedOn(Guid appId, int days)
        {
            var dt = DateTime.UtcNow.AddDays(days);                
            try
            {
                Drive.Data.Payments.Db.Applications.UpdateByExternalId(ExternalId: appId, AcceptedOn: dt);
            }
            catch (Exception)
            {
                Thread.Sleep(1000);
            Drive.Data.Payments.Db.Applications.UpdateByExternalId(ExternalId: appId, AcceptedOn: dt);
        }

        }

        private void UpdateNextDueDate(Guid appId, int days)
        {
            var dt = DateTime.UtcNow.AddDays(days).Date;
            var app = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Drive.Data.Payments.Db.FixedTermLoanApplications.UpdateByApplicationId(ApplicationId: app.ApplicationId, NextDueDate: dt);
        }

        private int TooEarlyToExtendDays()
        {
            // Check extend loan min days
            var cfg1 = Drive.Data.Ops.Db.ServiceConfigurations.FindByKey("Payments.ExtendLoanMinDays");
            return int.Parse(cfg1.Value) -1;
        }

        private int DueDateTooFarInFutureDays()
        {
            // Return number of days that will be 3 days too early from due date to be extended
            var cfg = Drive.Data.Ops.Db.ServiceConfigurations.FindByKey("Payments.ExtendLoanDaysBeforeDueDate");
            return int.Parse(cfg.Value) + 3;
        }

        private int NotDueDateTooFarInFutureDays()
        {
            // Return number of days that will be 3 days too early from due date to be extended
            var cfg = Drive.Data.Ops.Db.ServiceConfigurations.FindByKey("Payments.ExtendLoanDaysBeforeDueDate");
            return int.Parse(cfg.Value) - 3;
        }

        public void Scenario01Setup(Guid accountId, Guid appId, decimal trustRating)
        {
            var bankAccountId = Guid.NewGuid();
            var paymentCardId = Guid.NewGuid();
            
            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Guid id = appId;
            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == id));

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            
            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == id && a.SignedOn != null && a.AcceptedOn != null));
            
            // Go to DB and set Application to closed
            ApplicationEntity app = Drive.Db.Payments.Applications.Single(a => a.ExternalId == id);
            app.ClosedOn = DateTime.UtcNow.AddDays(-1);
            app.Submit(true);

        }

        public void Scenario02Setup(Guid appId, Guid paymentCardId, Guid bankAccountId, Guid accountId, decimal trustRating)
        {
            CheckExtensionIsEnabled();

            // Create Account
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });

            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });


            // Check App Exists in DB
            var db = Drive.Data.Payments.Db.Applications;
            Do.With.Interval(1).Until(() => db.Applications.Find(db.ExternalId == appId && db.SignedOn != null && db.AcceptedOn != null));

            // Alter NextDueDate & AcceptedOn
            var app = new Application(appId);
            app.NextDueDateTooEarlyToExtendLoan();
            app.AcceptedOnNotTooEarlyToExtend();

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 2);
        }

        public void Scenario03Setup(Guid appId, Guid paymentCardId, Guid bankAccountId, Guid accountId, decimal trustRating)
        {
            CheckExtensionIsEnabled();

            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });

            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });

            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Alter NextDueDate & AcceptedOn
            var app = new Application(appId);
            app.NextDueNotTooEarlyToExtendLoan();
            Thread.Sleep(300);
            app.AcceptedOnNotTooEarlyToExtend();

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With
                .Message(() => String.Format("there are currently {0} trans", Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count()))
                .Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 2);
        }

        /// <summary>
        /// Setup for MaxNoExtensionsReached
        /// </summary>
        public void Scenario04SetupMaxExtensionsReached(Guid paymentCardId, Guid appId, Guid bankAccountId, Guid accountId,decimal trustRating)
        {
            CheckExtensionIsEnabled();
            
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });

            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });

            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            CreateExtensionFeeTransaction(appId);
            CreateExtensionFeeTransaction(appId);
            CreateExtensionFeeTransaction(appId);

            // Alter NextDueDate & AcceptedOn
            var app = new Application(appId);
            app.NextDueNotTooEarlyToExtendLoan();
            app.AcceptedOnNotTooEarlyToExtend();

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 5);
        }

        /// <summary>
        /// Setup for Cannot Extend On Due Date
        /// </summary>
        public void Scenario04SetupCannotExtendOnDueDate(Guid paymentCardId, Guid appId, Guid bankAccountId, Guid accountId, decimal trustRating)
        {
            CheckExtensionIsEnabled();

            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });

            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });

            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Alter NextDueDate & AcceptedOn
            MakeAppDueToday(appId);
            MakeAppTooEarlyToExtendDays(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 2);
        }

        /// <summary>
        /// Setup for Cannot Extend On Day Before Due Date
        /// </summary>
        public void Scenario04SetupCannotExtendOnDayBeforeDueDate(Guid paymentCardId, Guid appId, Guid bankAccountId, Guid accountId, decimal trustRating)
        {
            CheckExtensionIsEnabled();

            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });

            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });

            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Alter NextDueDate & AcceptedOn
            MakeAppDueYesterday(appId);
            MakeAppTooEarlyToExtendDays(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 2);
        }

        public void Scenario05Setup(Guid paymentCardId, Guid appId, Guid bankAccountId, Guid accountId, decimal trustRating)
        {
            CheckExtensionIsEnabled();

            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });

            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });

            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Alter NextDueDate & AcceptedOn
            var app = new Application(appId);
            app.UpdateNextDueDate(DueDateTooFarInFutureDays());
            app.UpdateAcceptedOnDate(TooEarlyToExtendDays());

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 2);
        }

        public void Scenario06Setup(Guid appId, Guid paymentCardId, Guid bankAccountId, Guid accountId, decimal trustRating)
        {
            CheckExtensionIsEnabled();

            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });

            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });

            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Alter NextDueDate & AcceptedOn
            var app = new Application(appId);
            app.NextDueNotTooEarlyToExtendLoan();
            app.AcceptedOnNotTooEarlyToExtend();

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 2);
        }


        public void Scenario07Setup(Guid paymentCardId, Guid bankAccountId, Guid appId, Guid accountId, decimal trustRating)
        {
            CheckExtensionIsEnabled();

            // Create Account so that time zone can be looked up

            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });


            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });

            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Alter NextDueDate & AcceptedOn
            var app = new Application(appId);
            app.NextDueNotTooEarlyToExtendLoan();
            app.AcceptedOnNotTooEarlyToExtend();

            CreateExtensionFeeTransaction(appId);
            CreateExtensionFeeTransaction(appId);
            CreateExtensionFeeTransaction(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 5);
        }

        public void Scenario08Setup(Guid paymentCardId, Guid bankAccountId, Guid accountId, Guid appId)
        {
           
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });

            // Create Application 
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);

            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });

            // Check App Exists in DB
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 2);

            // Go to DB and set Application to closed
            Drive.Data.Payments.Db.Applications.UpdateByExternalId(ExternalId: appId,ClosedOn: DateTime.UtcNow.AddDays(0));
            // Set NextDueDate to Today
            var app = new Application(appId);
            app.UpdateNextDueDate(0);  

        }

        public void Scenario09Setup(Guid requestId2, Guid requestId1, Guid accountId, Guid paymentCardId, Guid appId, Guid bankAccountId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set SignedOn + AcceptedOn & check statuses have been updated
            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.SignedOn != null && a.AcceptedOn != null));

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 2);

            // Send command to create scheduled payment request
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId1, });
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId2, });

            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId1));
            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId2));

            // Go to DB and set Application NextDueDate to today.
            var app = new Application(appId);
            app.UpdateNextDueDate(0); 

        }

        public void Scenario10Setup(Guid requestId1, Guid requestId2, Guid appId, Guid bankAccountId, Guid accountId, Guid paymentCardId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.AccountPreferences.FindByAccountId(accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set SignedOn + AcceptedOn & check statuses have been updated
            var ts = DateTime.Now.AddHours(-1);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = ts });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = ts });
            // Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.SignedOn != null && a.AcceptedOn != null));
            var db = Drive.Data.Payments.Db.Applications;
            //var appTest = db.Find(db.ExternalId == appId && db.SignedOn != null && db.AcceptedOn != null);
            //Trace.WriteLine(">>>>>>>>>>>>>>>>>>>{0}" + appTest.ToString() + "asdasd");
            Do.With.Interval(1).Until(() => db.Find(db.ExternalId == appId && db.SignedOn != null && db.AcceptedOn != null));
            
            // Create transactions & Check transactions have been created
            CreateMissedPaymentChargeTransaction(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 3);

            // Send command to create scheduled payment request
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId1, });
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId2, });
            
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.RepaymentRequests.FindByExternalId(requestId1));
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.RepaymentRequests.FindByExternalId(requestId2));

            // Go to DB and set Application NextDueDate to yesterday.
            MakeAppDueYesterday(appId);
        }

        public void Scenario11Setup(Guid requestId1, Guid requestId2, Guid appId, Guid bankAccountId, Guid accountId, Guid paymentCardId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set SignedOn + AcceptedOn & check statuses have been updated
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.SignedOn != null && a.AcceptedOn != null));

            // Create transactions & Check transactions have been created
            CreateMissedPaymentChargeTransaction(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 3);

            // Send command to create scheduled payment request
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId1, });
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId2, });

            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId1));
            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId2));

            // Go to DB and set Application NextDueDate to 3 days ago.
            UpdateNextDueDate(appId, -3);
        }

        public void Scenario12Setup(Guid requestId1, Guid requestId2, Guid appId, Guid bankAccountId, Guid accountId, Guid paymentCardId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set SignedOn + AcceptedOn & check statuses have been updated
            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.SignedOn != null && a.AcceptedOn != null));

            // Create transactions & Check transactions have been created
            CreateMissedPaymentChargeTransaction(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 3);

            // Send command to create scheduled payment request
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId1, });
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId2, });

            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId1));
            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId2));

            // Go to DB and set Application NextDueDate to 31 days ago.
            UpdateNextDueDate(appId, -31);
        }

        public void Scenario13Setup(Guid requestId1, Guid requestId2, Guid appId, Guid bankAccountId, Guid accountId, Guid paymentCardId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set SignedOn + AcceptedOn & check statuses have been updated
            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.SignedOn != null && a.AcceptedOn != null));

            // Create transactions & Check transactions have been created
            CreateMissedPaymentChargeTransaction(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 3);

            // Send command to create scheduled payment request
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId1, });
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId2, });

            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId1));
            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId2));

            // Go to DB and set Application NextDueDate to yesterday.
            ApplicationEntity app = Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId);
            FixedTermLoanApplicationEntity fixedApp = Drive.Db.Payments.FixedTermLoanApplications.Single(a => a.ApplicationId == app.ApplicationId);
            fixedApp.NextDueDate = DateTime.UtcNow.Date.AddDays(-61);
            fixedApp.Submit(true);
        }

        public void Scenario14Setup(Guid requestId1, Guid requestId2, int applicationId, Guid accountId, Guid appId,Guid paymentCardId, Guid bankAccountId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set SignedOn + AcceptedOn & check statuses have been updated
            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.SignedOn != null && a.AcceptedOn != null));

            // Create transactions & Check transactions have been created
            CreateMissedPaymentChargeTransaction(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 3);

            // Send command to create scheduled payment request
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId1, });
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId2, });

            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId1));
            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId2));

            // Go to DB and set Application NextDueDate to yesterday.
            ApplicationEntity app = Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId);
            applicationId = app.ApplicationId;
            FixedTermLoanApplicationEntity fixedApp = Drive.Db.Payments.FixedTermLoanApplications.Single(a => a.ApplicationId == app.ApplicationId);
            fixedApp.NextDueDate = DateTime.UtcNow.Date.AddDays(-30);
            fixedApp.Submit(true);

            // Put application into arrears
            Drive.Msmq.Payments.Send(new AddArrearsCommand()
            {
                ApplicationId = applicationId,
                PaymentTransactionType = PaymentTransactionEnum.CardPayment,
                ReferenceId = Guid.NewGuid()
            });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Arrears.Single(a => a.ApplicationId == applicationId));

            // Create Repayment Arrangement
            var dateTimes = new DateTime[]
                                {
                                    DateTime.UtcNow.AddDays(10),
                                };
            Drive.Msmq.Payments.Send(new CreateRepaymentArrangementCommand()
            {
                ApplicationId = appId,
                Frequency = PaymentFrequencyEnum.Monthly,
                NumberOfMonths = 1,
                RepaymentDates = dateTimes,
                CreatedOn = DateTime.UtcNow
            });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentArrangements.Single(a => a.ApplicationId == applicationId));
        }

        public void Scenario15Setup(Guid requestId1, Guid requestId2, int applicationId, Guid accountId, Guid appId,Guid paymentCardId, Guid bankAccountId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set SignedOn + AcceptedOn & check statuses have been updated
            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.SignedOn != null && a.AcceptedOn != null));

            // Create transactions & Check transactions have been created
            CreateMissedPaymentChargeTransaction(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 3);

            // Send command to create scheduled payment request
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId1, });
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId2, });

            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId1));
            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId2));

            // Go to DB and set Application NextDueDate to yesterday.
            ApplicationEntity app = Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId);
            applicationId = app.ApplicationId;
            FixedTermLoanApplicationEntity fixedApp =
                Drive.Db.Payments.FixedTermLoanApplications.Single(a => a.ApplicationId == app.ApplicationId);
            fixedApp.NextDueDate = DateTime.UtcNow.Date.AddDays(-30);
            fixedApp.Submit(true);

            // Put application into arrears
            Drive.Msmq.Payments.Send(new AddArrearsCommand()
            {
                ApplicationId = applicationId,
                PaymentTransactionType = PaymentTransactionEnum.CardPayment,
                ReferenceId = Guid.NewGuid()
            });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Arrears.Single(a => a.ApplicationId == applicationId));

            // Create Repayment Arrangement
            DateTime dueDate = DateTime.UtcNow.AddDays(10).Date;
            var dateTimes = new DateTime[]
                                {
                                    dueDate,
                                };
            Drive.Msmq.Payments.Send(new CreateRepaymentArrangementCommand()
            {
                ApplicationId = appId,
                Frequency = PaymentFrequencyEnum.Monthly,
                NumberOfMonths = 1,
                RepaymentDates = dateTimes,
                CreatedOn = DateTime.UtcNow
            });

            RepaymentArrangementEntity ra = Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentArrangements.Single(a => a.ApplicationId == applicationId));

            // Put Repayment Arrangement into a state of missed payment
            RepaymentArrangementDetailEntity ras = Drive.Db.Payments.RepaymentArrangementDetails.Single(itm => itm.RepaymentArrangementId == ra.RepaymentArrangementId && itm.DueDate == dueDate);
            ras.DueDate = DateTime.UtcNow.AddDays(-1).Date;
            ras.Submit();
        }

        public void Scenario16Setup(Guid requestId1, Guid requestId2, int applicationId, Guid accountId, Guid appId,Guid paymentCardId, Guid bankAccountId)
        {

            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set SignedOn + AcceptedOn & check statuses have been updated
            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Thread.Sleep(250);
            Drive.Msmq.Payments.Send(new SignApplicationCommand() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.Now.AddHours(-1) });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.SignedOn != null && a.AcceptedOn != null));

            // Create transactions & Check transactions have been created
            CreateMissedPaymentChargeTransaction(appId);

            // Check transactions have been created
            var application = Drive.Data.Payments.Db.Applications.FindByExternalId(appId);
            Do.With.Interval(1).Until<Boolean>(() => Drive.Data.Payments.Db.Transactions.FindAllByApplicationId(application.ApplicationId).Count() == 3);

            // Send command to create scheduled payment request
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId1, });
            Drive.Msmq.Payments.Send(new CreateScheduledPaymentRequestCommand() { ApplicationId = appId, RepaymentRequestId = requestId2, });

            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId1));
            Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentRequests.Count(itm => itm.ExternalId == requestId2));

            // Go to DB and set Application NextDueDate to yesterday.
            ApplicationEntity app = Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId);
            applicationId = app.ApplicationId;
            FixedTermLoanApplicationEntity fixedApp =
                Drive.Db.Payments.FixedTermLoanApplications.Single(a => a.ApplicationId == app.ApplicationId);
            fixedApp.NextDueDate = DateTime.UtcNow.Date.AddDays(-61);
            fixedApp.Submit(true);

            // Put application into arrears
            Drive.Msmq.Payments.Send(new AddArrearsCommand()
            {
                ApplicationId = applicationId,
                PaymentTransactionType = PaymentTransactionEnum.CardPayment,
                ReferenceId = Guid.NewGuid()
            });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Arrears.Single(a => a.ApplicationId == applicationId));

            // Create Repayment Arrangement
            DateTime dueDate = DateTime.UtcNow.AddDays(10).Date;
            var dateTimes = new DateTime[]
                                {
                                    dueDate,
                                };
            Drive.Msmq.Payments.Send(new CreateRepaymentArrangementCommand()
            {
                ApplicationId = appId,
                Frequency = PaymentFrequencyEnum.Monthly,
                NumberOfMonths = 1,
                RepaymentDates = dateTimes,
                CreatedOn = DateTime.UtcNow
            });

            // Set Repayment Arrangement to Broken
            RepaymentArrangementEntity ra = Do.With.Interval(1).Until(() => Drive.Db.Payments.RepaymentArrangements.Single(a => a.ApplicationId == applicationId));
            ra.IsBroken = true;
            ra.Submit();

        }

        public void Scenario17Setup(Guid requestId1, Guid requestId2, int applicationId, Guid accountId, Guid appId,Guid paymentCardId, Guid bankAccountId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

        }

        public void Scenario20Setup(Guid requestId1, Guid requestId2, int applicationId, Guid accountId, Guid appId,Guid paymentCardId, Guid bankAccountId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set DeclinedOn & check status has been updated
            Drive.Msmq.Payments.Send(new IApplicationDeclinedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.UtcNow, FailedCheckpointName = "Test"});
            Do.With.Interval(1).Until(() =>Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.DeclinedOn != null));

        }

        public void Scenario21Setup(Guid requestId1, Guid requestId2, int applicationId, Guid accountId, Guid appId,Guid paymentCardId, Guid bankAccountId)
        {
            // Create Account so that time zone can be looked up
            Drive.Msmq.Payments.Send(new IAccountCreatedEvent() { AccountId = accountId });
            Do.With.Interval(1).Until(() => Drive.Db.Payments.AccountPreferences.Single(a => a.AccountId == accountId));

            // Create Application & Check it Exists in DB
            CreateFixedTermLoanApplication(appId, accountId, bankAccountId, paymentCardId);
            Do.With.Interval(1).Until(() => Drive.Data.Payments.Db.Applications.FindByExternalId(appId));

            // Set AcceptedOn & check status has been updated
            Drive.Msmq.Payments.Send(new IApplicationAcceptedEvent() { AccountId = accountId, ApplicationId = appId, CreatedOn = DateTime.UtcNow});
            Do.With.Interval(1).Until(() => Drive.Db.Payments.Applications.Single(a => a.ExternalId == appId && a.AcceptedOn != null));

        }

        private void CreateFixedTermLoanApplication(Guid appId, Guid accountId, Guid bankAccountId, Guid paymentCardId, int dueInDays = 10)
        {
            //Drive.Msmq.Payments.Send(new AddPaymentCardCommand()
            //{
            //    AccountId = accountId,
            //    CardType = "Visa Debit",
            //    IsCreditCard = false,
            //    ExpiryDateXml = "2014-10",
            //    SecurityCode = "123",
            //    IsPrimary = true,
            //    HolderName = "Mr Test Test",
            //    CreatedOn = DateTime.UtcNow,
            //    PaymentCardId = paymentCardId,
            //    Number = "4444333322221111"
            //});

            Drive.Msmq.Payments.Send(new AddBankAccountUkCommand()
            {
                AccountId = accountId,
                AccountNumber = "10032650",
                AccountOpenDate = DateTime.UtcNow.AddYears(-3),
                BankAccountId = bankAccountId,
                BankCode = "101627",
                BankName = "Royal Bank of Scotland",
                HolderName = "Mr Test Test",
                CountryCode = "UK",
                CreatedOn = DateTime.UtcNow
            });

            Drive.Msmq.Payments.Send(new CreateFixedTermLoanApplicationCommand()
            {
                ApplicationId = appId,
                AccountId = accountId,
                PromiseDate = DateTime.UtcNow.AddDays(dueInDays),
                BankAccountId = bankAccountId,
                PaymentCardId = paymentCardId,
                LoanAmount = 100.0M,
                Currency = CurrencyCodeIso4217Enum.GBP,
                CreatedOn = DateTime.UtcNow
            });

            Thread.Sleep(500);
            Drive.Msmq.Payments.Send(new IBankAccountValidatedEvent()
            {
                BankAccountId = bankAccountId,
                IsValid = true
            });

            
         
        }

        private Guid CreateLoanAdvanceTransaction(Guid appId)
        {
            var trnGuid1 = Guid.NewGuid();

            Drive.Msmq.Payments.Send(new CreateTransactionCommand()
            {
                ApplicationId = appId,
                ExternalId = trnGuid1,
                Amount = 100.00M,
                Type = PaymentTransactionEnum.CashAdvance,
                Currency = CurrencyCodeIso4217Enum.GBP,
                Mir = 30.0M,
                PostedOn = DateTime.Now,
                Scope = PaymentTransactionScopeEnum.Debit,
                Reference = "Test Cash Advance"
            });
            return trnGuid1;
        }

        private Guid CreateTransmissionFeeTransaction(Guid appId)
        {
            var trnGuid1 = Guid.NewGuid();

            Drive.Msmq.Payments.Send(new CreateTransactionCommand()
            {
                ApplicationId = appId,
                ExternalId = trnGuid1,
                Amount = 5.50M,
                Type = PaymentTransactionEnum.Fee,
                Currency = CurrencyCodeIso4217Enum.GBP,
                Mir = 30.0M,
                PostedOn = DateTime.Now,
                Scope = PaymentTransactionScopeEnum.Debit,
                Reference = "Test Transmission fee"
            });
            return trnGuid1;
        }

        private Guid CreateExtensionFeeTransaction(Guid appId)
        {
            var trnGuid1 = Guid.NewGuid();

            Drive.Msmq.Payments.Send(new CreateTransactionCommand()
            {
                ApplicationId = appId,
                ExternalId = trnGuid1,
                Amount = 20.00M,
                Type = PaymentTransactionEnum.LoanExtensionFee,
                Currency = CurrencyCodeIso4217Enum.GBP,
                Mir = 30.0M,
                PostedOn = DateTime.Now,
                Scope = PaymentTransactionScopeEnum.Debit,
                Reference = "Test Extension fee"
            });
            return trnGuid1;
        }

        private Guid CreateMissedPaymentChargeTransaction(Guid appId)
        {
            var trnGuid1 = Guid.NewGuid();

            Drive.Msmq.Payments.Send(new CreateTransactionCommand()
            {
                ApplicationId = appId,
                ExternalId = trnGuid1,
                Amount = 20.00M,
                Type = PaymentTransactionEnum.DefaultCharge,
                Currency = CurrencyCodeIso4217Enum.GBP,
                Mir = 30.0M,
                PostedOn = DateTime.Now,
                Scope = PaymentTransactionScopeEnum.Debit,
                Reference = "Test Missed Payment Charge"
            });
            return trnGuid1;
        }
    }
}
