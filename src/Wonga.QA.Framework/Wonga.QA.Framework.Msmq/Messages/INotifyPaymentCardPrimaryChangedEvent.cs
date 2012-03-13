using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Payments.PublicMessages.INotifyPaymentCardPrimaryChanged </summary>
    [XmlRoot("INotifyPaymentCardPrimaryChanged", Namespace = "Wonga.Payments.PublicMessages", DataType = "Wonga.Payments.PublicMessages.IPaymentsEvent")]
    public partial class INotifyPaymentCardPrimaryChangedEvent : MsmqMessage<INotifyPaymentCardPrimaryChangedEvent>
    {
        public Guid PaymentCardId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
