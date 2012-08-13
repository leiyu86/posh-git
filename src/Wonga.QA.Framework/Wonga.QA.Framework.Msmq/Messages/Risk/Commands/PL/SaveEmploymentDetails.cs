using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Wonga.QA.Framework.Msmq.Enums.Integration.Risk;

namespace Wonga.QA.Framework.Msmq.Messages.Risk.Commands.PL
{
    /// <summary> Wonga.Risk.Commands.PL.SaveEmploymentDetailsMessage </summary>
    [XmlRoot("SaveEmploymentDetailsMessage", Namespace = "Wonga.Risk.Commands.PL", DataType = "" )
    , SourceAssembly("Wonga.Risk.Commands.PL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class SaveEmploymentDetails : MsmqMessage<SaveEmploymentDetails>
    {
        public String UniversityCity { get; set; }
        public String UniversityName { get; set; }
        public OfUniversityPlEnum? UniversityType { get; set; }
        public String YearsAtEmployer { get; set; }
        public Int32 MonthsAtEmployer { get; set; }
        public Guid AccountId { get; set; }
        public EmploymentStatusEnum Status { get; set; }
        public String EmployerName { get; set; }
        public EmploymentIndustryEnum? EmploymentIndustry { get; set; }
        public EmploymentPositionEnum? EmploymentPosition { get; set; }
        public DateTime? StartDate { get; set; }
        public IncomeFrequencyEnum? IncomeFrequency { get; set; }
        public Decimal NetMonthlyIncome { get; set; }
        public DateTime? NextPayDate { get; set; }
        public Boolean? PaidDirectDeposit { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}
