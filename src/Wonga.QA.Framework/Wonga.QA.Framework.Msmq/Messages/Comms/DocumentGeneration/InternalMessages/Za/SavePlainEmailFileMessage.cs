using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.DocumentGeneration.InternalMessages.Za
{
    /// <summary> Wonga.Comms.DocumentGeneration.InternalMessages.Za.SavePlainEmailFileMessage </summary>
    [XmlRoot("SavePlainEmailFileMessage", Namespace = "Wonga.Comms.DocumentGeneration.InternalMessages.Za", DataType = "" )
    , SourceAssembly("Wonga.Comms.DocumentGeneration.InternalMessages.Za, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class SavePlainEmailFileMessage : MsmqMessage<SavePlainEmailFileMessage>
    {
        public Guid OriginatingSagaId { get; set; }
        public Byte[] Content { get; set; }
    }
}
