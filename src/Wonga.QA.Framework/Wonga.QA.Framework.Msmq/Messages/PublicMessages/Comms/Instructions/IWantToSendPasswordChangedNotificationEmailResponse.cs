using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.PublicMessages.Comms.Instructions
{
    /// <summary> Wonga.PublicMessages.Comms.Instructions.IWantToSendPasswordChangedNotificationEmailResponse </summary>
    [XmlRoot("IWantToSendPasswordChangedNotificationEmailResponse", Namespace = "Wonga.PublicMessages.Comms.Instructions", DataType = "" )
    , SourceAssembly("Wonga.PublicMessages.Comms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IWantToSendPasswordChangedNotificationEmailResponse : MsmqMessage<IWantToSendPasswordChangedNotificationEmailResponse>
    {
        public Guid SagaId { get; set; }
    }
}
