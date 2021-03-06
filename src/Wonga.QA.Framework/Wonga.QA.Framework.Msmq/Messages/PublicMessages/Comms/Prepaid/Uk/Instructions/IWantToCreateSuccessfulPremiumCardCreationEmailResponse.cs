using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.PublicMessages.Comms.Prepaid.Uk.Instructions
{
    /// <summary> Wonga.PublicMessages.Comms.Prepaid.Uk.Instructions.IWantToCreateSuccessfulPremiumCardCreationEmailResponse </summary>
    [XmlRoot("IWantToCreateSuccessfulPremiumCardCreationEmailResponse", Namespace = "Wonga.PublicMessages.Comms.Prepaid.Uk.Instructions", DataType = "" )
    , SourceAssembly("Wonga.PublicMessages.Comms.Prepaid.Uk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IWantToCreateSuccessfulPremiumCardCreationEmailResponse : MsmqMessage<IWantToCreateSuccessfulPremiumCardCreationEmailResponse>
    {
        public Guid DocumentId { get; set; }
        public Guid AccountId { get; set; }
    }
}
