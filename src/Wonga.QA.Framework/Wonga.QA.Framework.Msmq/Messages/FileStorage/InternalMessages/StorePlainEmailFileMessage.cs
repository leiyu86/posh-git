using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.FileStorage.InternalMessages
{
    /// <summary> Wonga.FileStorage.InternalMessages.StorePlainEmailFileMessage </summary>
    [XmlRoot("StorePlainEmailFileMessage", Namespace = "Wonga.FileStorage.InternalMessages", DataType = "" )
    , SourceAssembly("Wonga.FileStorage.InternalMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class StorePlainEmailFileMessage : MsmqMessage<StorePlainEmailFileMessage>
    {
        public Guid OriginatingSagaId { get; set; }
        public String TextContent { get; set; }
    }
}
