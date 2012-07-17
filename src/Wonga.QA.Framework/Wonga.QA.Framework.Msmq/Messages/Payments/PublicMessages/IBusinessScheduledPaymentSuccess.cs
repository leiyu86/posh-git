using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Payments.PublicMessages
{
    /// <summary> Wonga.Payments.PublicMessages.IBusinessScheduledPaymentSuccess </summary>
    [XmlRoot("IBusinessScheduledPaymentSuccess", Namespace = "Wonga.Payments.PublicMessages", DataType = "Wonga.Payments.PublicMessages.IPaymentsEvent")]
    public partial class IBusinessScheduledPaymentSuccess : MsmqMessage<IBusinessScheduledPaymentSuccess>
    {
        public Guid OrganisationId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid? TransactionId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}