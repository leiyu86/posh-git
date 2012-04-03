﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Data.Enums.Risk;
using Wonga.QA.Framework.Db.Extensions;
using Wonga.QA.Framework.Db.Risk;
using Wonga.QA.Tests.Core;

namespace Wonga.QA.Tests.CallValidate
{
    public class CheckpointsTests
    {
        private const String GoodCompanyRegNumber = "00000086";

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-136")]
        public void TestCallValidateMainApplicantPaymentCardIsValid_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "bridson";

            var mainApplicantBuilder =
                CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithPaymentCardNumber(
                    Int64.Parse("1111222233334444")).WithMiddleName(RiskMask.TESTCallValidatePaymentCardIsValid);


            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Verified, 1);

            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.PaymentCardIsValid,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CallValidatePaymentCardIsValidVerification);
        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-136")]
        public void TestCallValidateMainApplicantPaymentCardIsNotValid_LoanIsDeclined()
        {
            const String forename = "kathleen";
            const String surname = "bridson";

            var mainApplicantBuilder =
                CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithPaymentCardNumber(
                    Int64.Parse("9999888877776666")).WithMiddleName(RiskMask.TESTCallValidatePaymentCardIsValid);

            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Declined);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Failed, 1);

            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.PaymentCardIsValid,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.CallValidatePaymentCardIsValidVerification);
        }

        private static Application CreateApplicationWithAsserts(CustomerBuilder mainApplicantBuilder, String companyRegisteredNumber, ApplicationDecisionStatus applicationDecision, List<CustomerBuilder> guarantors = null)
        {
            mainApplicantBuilder.ScrubForename(mainApplicantBuilder.Forename);
            mainApplicantBuilder.ScrubSurname(mainApplicantBuilder.Surname);

            //STEP 1 - Create the main director
            var mainDirector = mainApplicantBuilder.Build();

            //STEP2 - Create the company
            var organisationBuilder = OrganisationBuilder.New(mainDirector).WithOrganisationNumber(companyRegisteredNumber);
            var organisation = organisationBuilder.Build();

            //STEP3 - Create the application
            var applicationBuilder = ApplicationBuilder.New(mainDirector, organisation).WithExpectedDecision(applicationDecision) as BusinessApplicationBuilder;

            //STEP4 - Create the guarantors list + send it to the application
            if (guarantors != null)
            {
                applicationBuilder.WithGuarantors(guarantors);
            }

            //STEP5 - Build the application + send the list of guarantors
            var application = applicationBuilder.Build();

            Assert.IsNotNull(application);

            var riskDb = Drive.Db.Risk;
            var riskApplicationEntity = Do.Until(() => riskDb.RiskApplications.SingleOrDefault(p => p.ApplicationId == application.Id));
            Assert.IsNotNull(riskApplicationEntity, "Risk application should exist");

            var riskAccountEntity = Do.Until(() => riskDb.RiskAccounts.SingleOrDefault(p => p.AccountId == riskApplicationEntity.AccountId));
            Assert.IsNotNull(riskAccountEntity, "Risk account should exist");

            var socialDetailsEntity = Do.Until(() => riskDb.SocialDetails.SingleOrDefault(p => p.AccountId == riskApplicationEntity.AccountId));
            Assert.IsNotNull(socialDetailsEntity, "Risk Social details should exist");

            return application;

        }
        private List<RiskWorkflowEntity> VerifyRiskWorkflows(Guid applicationId, RiskWorkflowTypes riskWorkflowType, RiskWorkflowStatus expectedRiskWorkflowStatus, Int32 expectedNumberOfWorkflows)
        {
            Drive.Db.WaitForRiskWorkflowData(applicationId, riskWorkflowType, expectedNumberOfWorkflows, expectedRiskWorkflowStatus);
            var riskWorkflows = Drive.Db.GetWorkflowsForApplication(applicationId, riskWorkflowType);
            Assert.AreEqual(expectedNumberOfWorkflows, riskWorkflows.Count, "There should be " + expectedNumberOfWorkflows + " workflows");

            foreach (var riskWorkflow in riskWorkflows)
            {
                Assert.AreEqual(expectedRiskWorkflowStatus, (RiskWorkflowStatus)riskWorkflow.Decision);
            }

            return riskWorkflows;
        }
        private void VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(RiskWorkflowEntity riskWorkflowEntity, RiskCheckpointDefinitionEnum checkpointDefinition, RiskCheckpointStatus checkpointStatus, RiskVerificationDefinitions riskVerification)
        {
            Do.Until(() => Drive.Db.Risk.WorkflowCheckpoints.Any(p => p.RiskWorkflowId == riskWorkflowEntity.RiskWorkflowId && p.CheckpointStatus != 0));
            Assert.Contains(Drive.Db.GetExecutedCheckpointDefinitionNamesForRiskWorkflow(riskWorkflowEntity.WorkflowId, checkpointStatus), Get.EnumToString(checkpointDefinition));
            Assert.Contains(Drive.Db.GetExecutedVerificationDefinitionNamesForRiskWorkflow(riskWorkflowEntity.WorkflowId), Get.EnumToString(riskVerification));
        }

    }
}
