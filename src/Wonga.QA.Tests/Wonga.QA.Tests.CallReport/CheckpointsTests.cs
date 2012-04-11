﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Data.Enums.Risk;
using Wonga.QA.Framework.Db;
using Wonga.QA.Framework.Db.Extensions;
using Wonga.QA.Framework.Db.Risk;
using Wonga.QA.Tests.Core;
using System.Threading;

namespace Wonga.QA.Tests.CallReport
{
    public class CheckpointsTests
    {
        private const String GoodCompanyRegNumber = "00000086";

        #region Main Applicant
        
        /* Main Appplicant Is Alive */

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-575"), Description("CallReport -> This test creates a loan for the unknown customer that is alive and with no consumer bureau data, then checks the risk checkpoint")]
        public void TestCallReportUnknownMainApplicant_LoanIsApproved()
        {
            const String forename = "unknown";
            const String surname = "customer";

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsNotDeceased);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.ApplicantIsAlive,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsAliveVerification);
        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-575"), Description("CallReport -> This test creates a loan for the Kathleen customer that is alive according to call report, then checks the risk checkpoint")]
        public void TestCallReportMainApplicantIsNotDeceased_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "bridson";

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsNotDeceased);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.ApplicantIsAlive,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsAliveVerification);
        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-575"), Description("CallReport -> This test creates a loan for the customer that is dead according to call report, then checks the risk checkpoint")]
        public void TestCallReportMainApplicantIsDeceased_LoanIsDeclined()
        {
            const String forename = "Johnny";
            const String surname = "DeadGuy";

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsNotDeceased);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Declined);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Failed, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.ApplicantIsAlive,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsAliveVerification);
        }

        /* Main Applicant CIFAS check */

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-584"), Description("CallReport -> This test creates a loan for a customer that is not CIFAS flagged, then checks the risk checkpoint")]
        public void TestCallReportMainApplicantIsNotCifasFlagged_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "Bridson";

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicationElementNotCIFASFlagged);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CIFASFraudCheck,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauCifasFraudCheckVerification);

        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-584"), Description("CallReport -> This test creates a loan for a customer that IS CIFAS flagged, then checks the risk checkpoint")]
        public void TestCallReportMainApplicantIsCifasFlagged_LoanIsDeclined()
        {
            const String forename = "laura";
            const String surname = "insolvent";

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicationElementNotCIFASFlagged);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Declined);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Failed, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CIFASFraudCheck,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.CreditBureauCifasFraudCheckVerification);
        }

        /* Main Applicant Data is Available */

        [Test, AUT(AUT.Wb)]
        [Description("Callreport -> This test creates a loan and checks if the main applicant has data available")]
        public void TestCallReportMainApplicantDataIsAvailable_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "bridson";

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTCreditBureauDataIsAvailable);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CreditBureauDataIsAvailable,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauDataIsAvailableVerification);
        }

        [Test, AUT(AUT.Wb),]
        [Description("Callreport -> This test creates a loan and checks if the main applicant has data available")]
        public void TestCallReportMainApplicantIsNotAvailable_LoanIsDeclined()
        {
            const String forename = "Unknown";
            const String surname = "Customer";

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTCreditBureauDataIsAvailable);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Declined);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Failed, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CreditBureauDataIsAvailable,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.CreditBureauDataIsAvailableVerification);
        }

        /* Main Applicant is Insolvent */

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-638"), Description("CallReport -> This test creates a loan for the solvent customer, then checks the risk checkpoint")]
        public void TestCallReportMainApplicantIsSolvent_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "Bridson";

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsSolvent);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Verified, 1);

            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CustomerIsSolvent,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsSolventVerification);
        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-638"), Description("CallReport -> This test creates a loan for the insolvent customer, then checks the risk checkpoint")]
        public void TestCallReportMainApplicantIsInsolvent_LoanIsDeclined()
        {
            const String forename = "laura";
            const String surname = "insolvent";

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsSolvent);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Declined);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Failed, 1);

            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CustomerIsSolvent,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsSolventVerification);

        }

        /* Main applicant DOB is correct */

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-644"), Description("CallReport -> This test creates a loan for a customer with the correct date of birth, then checks the risk checkpoint")]
        public void TestCallReportMainApplicantDateOfBirthIsCorrect_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "bridson";
            var dateOfBirth = new Date(new DateTime(1992, 1, 24), DateFormat.Date);

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithDateOfBirth(dateOfBirth).WithMiddleName(RiskMask.TESTCustomerDateOfBirthIsCorrectSME);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.DateOfBirthIsCorrect,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.DateOfBirthIsCorrectVerification);
        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-644"), Description("CallReport -> This test creates a loan for a customer with the incorrect date of birth, then checks the risk checkpoint")]
        public void TestCallReportMainApplicantDateOfBirthIsIncorrect_LoanIsDeclined()
        {
            const String forename = "kathleen";
            const String surname = "bridson";
            var dateOfBirth = new Date(new DateTime(1990, 3, 21), DateFormat.Date);

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithDateOfBirth(dateOfBirth).WithMiddleName(RiskMask.TESTCustomerDateOfBirthIsCorrectSME);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Declined);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Failed, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.DateOfBirthIsCorrect,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.DateOfBirthIsCorrectVerification);

        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-644"), Description("CallReport -> This test creates a loan for a customer with the not provided date of birth, then checks the risk checkpoint")]
        public void TestCallReportMainApplicantDateOfBirthNotProvided_LoanIsApproved()
        {
            const String forename = "unknown";
            const String surname = "customer";
            var wrongDateOfBirth = new Date(new DateTime(1973, 5, 11), DateFormat.Date);

            var mainApplicantBuilder = CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithDateOfBirth(wrongDateOfBirth).WithMiddleName(RiskMask.TESTCustomerDateOfBirthIsCorrectSME);
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted);

            var mainApplicantRiskWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.MainApplicant, RiskWorkflowStatus.Verified, 1);

            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(mainApplicantRiskWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.DateOfBirthIsCorrect,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.DateOfBirthIsCorrectVerification);

        }

        #endregion

        #region Guarantor

        /* Guarantor is Alive */
        
        [Test, AUT(AUT.Wb)]
        [JIRA("SME-1147"), Description("CallReport -> This test creates a loan for the unknown guarantor that is alive, then checks the risk checkpoint")]
        public void TestCallReportUnknownGuarantor_LoanIsApproved()
        {
            const String forename = "unknown";
            const String surname = "customer";

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsNotDeceased),
                                    };

            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted, guarantorList);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.ApplicantIsAlive,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsAliveVerification);
        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-1147"), Description("CallReport -> This test creates a loan for the unknown guarantor that is alive, then checks the risk checkpoint")]
        public void TestCallReportGuarantorIsAlive_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "bridson";

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsNotDeceased),
                                    };
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted, guarantorList);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.ApplicantIsAlive,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsAliveVerification);
        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-1147"), Description("CallReport -> This test creates a loan for the unknown guarantor that is alive, then checks the risk checkpoint")]
        public void TestCallReportGuarantorIsDeceased_LoanIsDeclined()
        {
            const String forename = "Johnny";
            const String surname = "DeadGuy";

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsNotDeceased),
                                    };


            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.PreAccepted, guarantorList);
            Do.Until(() => (ApplicationDecisionStatus)Enum.Parse(typeof(ApplicationDecisionStatus), Drive.Api.Queries.Post(new GetApplicationDecisionQuery { ApplicationId = application.Id }).Values["ApplicationDecisionStatus"].Single()) == ApplicationDecisionStatus.Declined);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Failed, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.ApplicantIsAlive,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsAliveVerification);
        }

        /* Guarantor CIFAS check */

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-1144"), Description("CallReport -> This test creates a loan for a guarantor that is not CIFAS flagged, then checks the risk checkpoint")]
        public void TestCallReportGuarantorIsNotCifasFlagged_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "Bridson";

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicationElementNotCIFASFlagged),
                                    };

            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted, guarantorList);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CIFASFraudCheck,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauCifasFraudCheckVerification);

        }

        [Test, AUT(AUT.Wb)]
        [JIRA("SME-1144"), Description("CallReport -> This test creates a loan for a guarantor that is CIFAS flagged, then checks the risk checkpoint")]
        public void TestCallReportGuarantorIsCifasFlagged_LoanIsDeclined()
        {
            const String forename = "laura";
            const String surname = "insolvent";

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicationElementNotCIFASFlagged),
                                    };

            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.PreAccepted, guarantorList);
            Do.Until(() => (ApplicationDecisionStatus)Enum.Parse(typeof(ApplicationDecisionStatus), Drive.Api.Queries.Post(new GetApplicationDecisionQuery { ApplicationId = application.Id }).Values["ApplicationDecisionStatus"].Single()) == ApplicationDecisionStatus.Declined);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Failed, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CIFASFraudCheck,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.CreditBureauCifasFraudCheckVerification);

        }

        /* Guarantor Data is available */

        [Test, AUT(AUT.Wb), JIRA("SME-1141")]
        [Description("Callreport -> This test creates a loan and checks if the guarantors has data available")]
        public void TestCallReportGuarantorDataIsAvailable_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "bridson";

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTCreditBureauDataIsAvailable),
                                    };

            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted, guarantorList);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CreditBureauDataIsAvailable,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauDataIsAvailableVerification);
        }

        [Test, AUT(AUT.Wb), JIRA("SME-1141")]
        [Description("Callreport -> This test creates a loan and checks if the guarantors has data available")]
        public void TestCallReportGuarantorDataIsNotAvailable_LoanIsDeclined()
        {
            const String forename = "Unknown";
            const String surname = "Customer";

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTCreditBureauDataIsAvailable),
                                    };

            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.PreAccepted, guarantorList);
            Do.Until(() => (ApplicationDecisionStatus)Enum.Parse(typeof(ApplicationDecisionStatus), Drive.Api.Queries.Post(new GetApplicationDecisionQuery { ApplicationId = application.Id }).Values["ApplicationDecisionStatus"].Single()) == ApplicationDecisionStatus.Declined);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Failed, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CreditBureauDataIsAvailable,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.CreditBureauDataIsAvailableVerification);
        }

        /* Guarantor is solvent */

        [Test, AUT(AUT.Wb), JIRA("SME-1150")]
        [Description("Callreport -> This test creates a loan and checks if the guarantors is solvent")]
        public void TestCallReportGuarantorIsSolvent_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "bridson";

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsSolvent),
                                    };
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted, guarantorList);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CustomerIsSolvent,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsSolventVerification);
        }

        [Test, AUT(AUT.Wb), JIRA("SME-1150")]
        [Description("Callreport -> This test creates a loan and checks if the guarantors is solvent")]
        public void TestCallReportGuarantorIsInsolvent_LoanIsDeclined()
        {
            const String forename = "laura";
            const String surname = "insolvent";

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTApplicantIsSolvent),
                                    };

            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.PreAccepted, guarantorList);
            Do.Until(() => (ApplicationDecisionStatus)Enum.Parse(typeof(ApplicationDecisionStatus), Drive.Api.Queries.Post(new GetApplicationDecisionQuery { ApplicationId = application.Id }).Values["ApplicationDecisionStatus"].Single()) == ApplicationDecisionStatus.Declined);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Failed, 1);

            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.CustomerIsSolvent,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.CreditBureauCustomerIsSolventVerification);
        }

        /* Guarantor DOB is correct */

        [Test, AUT(AUT.Wb), JIRA("SME-1138")]
        [Description("Callreport -> This test creates a loan and checks if the guarantors entered the correct DOB")]
        public void TestCallReportGuarantorDateOfBirthIsCorrect_LoanIsApproved()
        {
            const String forename = "kathleen";
            const String surname = "bridson";
            var dateOfBirth = new Date(new DateTime(1992, 1, 24), DateFormat.Date);

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTCustomerDateOfBirthIsCorrectSME).WithDateOfBirth(dateOfBirth),
                                    };
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted, guarantorList);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.DateOfBirthIsCorrect,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.DateOfBirthIsCorrectVerification);
        }

        [Test, AUT(AUT.Wb), JIRA("SME-1138")]
        [Description("Callreport -> This test creates a loan and checks if the guarantors entered the correct DOB")]
        public void TestCallReportGuarantorDateOfBirthNotProvided_LoanIsApproved()
        {
            const String forename = "unknown";
            const String surname = "customer";
            
            var dateOfBirth = new Date(new DateTime(1973, 5, 11), DateFormat.Date);

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTCustomerDateOfBirthIsCorrectSME).WithDateOfBirth(dateOfBirth),
                                    };
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.Accepted, guarantorList);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Verified, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.DateOfBirthIsCorrect,
                                                                     RiskCheckpointStatus.Verified,
                                                                     RiskVerificationDefinitions.DateOfBirthIsCorrectVerification);
        }

        [Test, AUT(AUT.Wb), JIRA("SME-1138")]
        [Description("Callreport -> This test creates a loan and checks if the guarantors entered the correct DOB")]
        public void TestCallReportGuarantorDateOfBirthIsInCorrect_LoanIsDeclined()
        {
            const String forename = "kathleen";
            const String surname = "bridson";
            var dateOfBirth = new Date(new DateTime(1990, 1, 24), DateFormat.Date);

            var mainApplicantBuilder = CustomerBuilder.New();
            var guarantorList = new List<CustomerBuilder>
                                    {
                                        CustomerBuilder.New().WithForename(forename).WithSurname(surname).WithMiddleName(RiskMask.TESTCustomerDateOfBirthIsCorrectSME).WithDateOfBirth(dateOfBirth),
                                    };
            var application = CreateApplicationWithAsserts(mainApplicantBuilder, GoodCompanyRegNumber, ApplicationDecisionStatus.PreAccepted, guarantorList);
            Do.Until(() => (ApplicationDecisionStatus)Enum.Parse(typeof(ApplicationDecisionStatus), Drive.Api.Queries.Post(new GetApplicationDecisionQuery { ApplicationId = application.Id }).Values["ApplicationDecisionStatus"].Single()) == ApplicationDecisionStatus.Declined);

            var guarantorWorkflows = VerifyRiskWorkflows(application.Id, RiskWorkflowTypes.Guarantor, RiskWorkflowStatus.Failed, 1);
            VerifyCheckpointDefinitionAndVerificationForRiskWorkflow(guarantorWorkflows[0],
                                                                     RiskCheckpointDefinitionEnum.DateOfBirthIsCorrect,
                                                                     RiskCheckpointStatus.Failed,
                                                                     RiskVerificationDefinitions.DateOfBirthIsCorrectVerification);
        }

        #endregion

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

                foreach (var customerBuilder in guarantors)
                {
                    customerBuilder.ScrubForename(customerBuilder.Forename);
                    customerBuilder.ScrubSurname(customerBuilder.Surname);
                }
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
            Drive.Db.WaitForRiskWorkflowData(applicationId, riskWorkflowType, expectedNumberOfWorkflows,expectedRiskWorkflowStatus);
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
            Drive.Db.WaitForWorkflowCheckpointData(riskWorkflowEntity.RiskWorkflowId);
            Assert.Contains(Drive.Db.GetExecutedCheckpointDefinitionNamesForRiskWorkflow(riskWorkflowEntity.WorkflowId, checkpointStatus), Get.EnumToString(checkpointDefinition));
            Assert.Contains(Drive.Db.GetExecutedVerificationDefinitionNamesForRiskWorkflow(riskWorkflowEntity.WorkflowId), Get.EnumToString(riskVerification));
        }
    }
}
