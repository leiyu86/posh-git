using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Comms.InternalMessages.FileStorage.SaveFileMessage </summary>
    [XmlRoot("SaveFileMessage", Namespace = "Wonga.Comms.InternalMessages.FileStorage", DataType = "Wonga.Comms.InternalMessages.SagaMessages.BaseSagaMessage,NServiceBus.Saga.ISagaMessage")]
    public partial class SaveFileCommsCommand : MsmqMessage<SaveFileCommsCommand>
    {
        public Guid FileId { get; set; }
        public Byte[] Content { get; set; }
        public Guid SagaId { get; set; }
    }
}
