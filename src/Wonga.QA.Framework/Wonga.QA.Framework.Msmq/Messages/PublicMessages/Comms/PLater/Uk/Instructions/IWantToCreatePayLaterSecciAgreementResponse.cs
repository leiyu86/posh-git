using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.PublicMessages.Comms.PLater.Uk.Instructions
{
    /// <summary> Wonga.PublicMessages.Comms.PLater.Uk.Instructions.IWantToCreatePayLaterSecciAgreementResponse </summary>
    [XmlRoot("IWantToCreatePayLaterSecciAgreementResponse", Namespace = "Wonga.PublicMessages.Comms.PLater.Uk.Instructions", DataType = "" )
    , SourceAssembly("Wonga.PublicMessages.Comms.PLater.Uk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IWantToCreatePayLaterSecciAgreementResponse : MsmqMessage<IWantToCreatePayLaterSecciAgreementResponse>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid DocumentId { get; set; }
    }
}
