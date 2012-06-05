using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.PublicMessages.Comms.Instructions.IWantToSendExtensionAgreedEmailResponse </summary>
    [XmlRoot("IWantToSendExtensionAgreedEmailResponse", Namespace = "Wonga.PublicMessages.Comms.Instructions", DataType = "")]
    public partial class IWantToSendExtensionAgreedEmailResponseEvent : MsmqMessage<IWantToSendExtensionAgreedEmailResponseEvent>
    {
        public Guid SagaId { get; set; }
    }
}
