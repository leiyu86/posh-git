using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Payments.Csapi.Commands.CsExtendFixedTermLoan </summary>
    [XmlRoot("CsExtendFixedTermLoan", Namespace = "Wonga.Payments.Csapi.Commands", DataType = "")]
    public partial class CsExtendFixedTermLoanCsCommand : MsmqMessage<CsExtendFixedTermLoanCsCommand>
    {
        public Guid ApplicationId { get; set; }
        public Guid PaymentCardId { get; set; }
        public Guid LoanExtensionId { get; set; }
        public String CV2 { get; set; }
        public Decimal PartPaymentAmount { get; set; }
        public Int32 AgentId { get; set; }
        public DateTime ExtensionDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}