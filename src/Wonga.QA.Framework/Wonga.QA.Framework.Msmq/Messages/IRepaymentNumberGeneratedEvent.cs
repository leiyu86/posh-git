using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Payments.PublicMessages.IRepaymentNumberGenerated </summary>
    [XmlRoot("IRepaymentNumberGenerated", Namespace = "Wonga.Payments.PublicMessages", DataType = "Wonga.Payments.PublicMessages.IPaymentsEvent")]
    public partial class IRepaymentNumberGeneratedEvent : MsmqMessage<IRepaymentNumberGeneratedEvent>
    {
        public Guid AccountId { get; set; }
        public String RepaymentNumber { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
