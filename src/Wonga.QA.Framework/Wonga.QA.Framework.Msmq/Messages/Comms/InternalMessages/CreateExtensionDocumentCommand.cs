using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages
{
    /// <summary> Wonga.Comms.InternalMessages.CreateExtensionDocumentMessage </summary>
    [XmlRoot("CreateExtensionDocumentMessage", Namespace = "Wonga.Comms.InternalMessages", DataType = "")]
    public partial class CreateExtensionDocumentCommand : MsmqMessage<CreateExtensionDocumentCommand>
    {
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public Guid ExtensionId { get; set; }
    }
}