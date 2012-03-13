using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Payments.PublicMessages.INegativeCounterOfferAccepted </summary>
    [XmlRoot("INegativeCounterOfferAccepted", Namespace = "Wonga.Payments.PublicMessages", DataType = "Wonga.Payments.PublicMessages.IPaymentsEvent")]
    public partial class INegativeCounterOfferAcceptedEvent : MsmqMessage<INegativeCounterOfferAcceptedEvent>
    {
        public Guid ApplicationId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
