using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Payments.RepayLoanViaBank </summary>
    [XmlRoot("RepayLoanViaBank", Namespace = "Wonga.Payments", DataType = "")]
    public partial class RepayLoanViaBankCommand : MsmqMessage<RepayLoanViaBankCommand>
    {
        public Guid ApplicationId { get; set; }
        public Guid? CashEntityId { get; set; }
        public Decimal? Amount { get; set; }
        public DateTime ActionDate { get; set; }
        public Guid RepaymentRequestId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}
