using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.DocumentGeneration.Wb.Uk.PaymentRequestFailures
{
    /// <summary> Wonga.Comms.InternalMessages.DocumentGeneration.Wb.Uk.PaymentRequestFailures.CreateAndStoreFirstPaymentFailedEmailMessage </summary>
    [XmlRoot("CreateAndStoreFirstPaymentFailedEmailMessage", Namespace = "Wonga.Comms.InternalMessages.DocumentGeneration.Wb.Uk.PaymentRequestFailures", DataType = "")]
    public partial class CreateAndStoreFirstPaymentFailedEmailWbUkCommand : MsmqMessage<CreateAndStoreFirstPaymentFailedEmailWbUkCommand>
    {
        public Guid ApplicationId { get; set; }
        public Guid OrganisationId { get; set; }
    }
}