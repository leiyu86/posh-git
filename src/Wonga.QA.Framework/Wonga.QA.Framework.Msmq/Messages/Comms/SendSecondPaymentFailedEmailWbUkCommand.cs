using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Comms
{
    /// <summary> Wonga.Comms.InternalMessages.Email.Wb.Uk.PaymentRequestFailures.SendSecondPaymentFailedEmailMessage </summary>
    [XmlRoot("SendSecondPaymentFailedEmailMessage", Namespace = "Wonga.Comms.InternalMessages.Email.Wb.Uk.PaymentRequestFailures", DataType = "")]
    public partial class SendSecondPaymentFailedEmailWbUkCommand : MsmqMessage<SendSecondPaymentFailedEmailWbUkCommand>
    {
        public Guid ApplicationId { get; set; }
        public Guid OrganisationId { get; set; }
        public Guid FileId { get; set; }
        public String EmailAddress { get; set; }
    }
}
