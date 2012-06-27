using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.Email
{
    /// <summary> Wonga.Comms.InternalMessages.Email.SendExtensionWindowOpenEmailMessage </summary>
    [XmlRoot("SendExtensionWindowOpenEmailMessage", Namespace = "Wonga.Comms.InternalMessages.Email", DataType = "Wonga.Comms.InternalMessages.SagaMessages.BaseSagaMessage,NServiceBus.Saga.ISagaMessage")]
    public partial class SendExtensionWindowOpenEmailCommand : MsmqMessage<SendExtensionWindowOpenEmailCommand>
    {
        public Guid AccountId { get; set; }
        public String Email { get; set; }
        public String Forename { get; set; }
        public String PageUrl { get; set; }
        public Guid SagaId { get; set; }
    }
}