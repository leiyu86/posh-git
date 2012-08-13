using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.Ca.BritishColombia
{
    /// <summary> Wonga.Comms.InternalMessages.Ca.BritishColombia.CreateBcAgreeementDocumentMessage </summary>
    [XmlRoot("CreateBcAgreeementDocumentMessage", Namespace = "Wonga.Comms.InternalMessages.Ca.BritishColombia", DataType = "" )
    , SourceAssembly("Wonga.Comms.InternalMessages.Ca, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class CreateBcAgreeementDocumentMessage : MsmqMessage<CreateBcAgreeementDocumentMessage>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
