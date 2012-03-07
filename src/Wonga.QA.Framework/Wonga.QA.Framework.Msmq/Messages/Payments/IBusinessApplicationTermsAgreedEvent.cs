using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Payments
{
    /// <summary> Wonga.Payments.PublicMessages.IBusinessApplicationTermsAgreed </summary>
    [XmlRoot("IBusinessApplicationTermsAgreed", Namespace = "Wonga.Payments.PublicMessages", DataType = "Wonga.Payments.PublicMessages.IPaymentsEvent")]
    public partial class IBusinessApplicationTermsAgreedEvent : MsmqMessage<IBusinessApplicationTermsAgreedEvent>
    {
        public Guid ApplicationId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
