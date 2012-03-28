using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.PublicMessages.Comms.Instructions.IWantToSendSmsResponse </summary>
    [XmlRoot("IWantToSendSmsResponse", Namespace = "Wonga.PublicMessages.Comms.Instructions", DataType = "NServiceBus.Saga.ISagaMessage")]
    public partial class IWantToSendSmsResponseEvent : MsmqMessage<IWantToSendSmsResponseEvent>
    {
        public Boolean Successful { get; set; }
        public Guid SagaId { get; set; }
    }
}