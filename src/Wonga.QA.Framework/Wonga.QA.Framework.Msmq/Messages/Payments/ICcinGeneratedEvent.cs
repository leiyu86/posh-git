using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Payments
{
    [XmlRoot("ICcinGenerated", Namespace = "Wonga.Payments.PublicMessages", DataType = "Wonga.Payments.PublicMessages.IPaymentsEvent")]
    public partial class ICcinGeneratedEvent : MsmqMessage<ICcinGeneratedEvent>
    {
        public Guid AccountId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
