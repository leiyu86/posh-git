using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.PublicMessages.Comms.Ca.Instructions.BritishColumbia
{
    /// <summary> Wonga.PublicMessages.Comms.Ca.Instructions.BritishColumbia.IWantToGenerateAPreAuthorisedDebitDocument </summary>
    [XmlRoot("IWantToGenerateAPreAuthorisedDebitDocument", Namespace = "Wonga.PublicMessages.Comms.Ca.Instructions.BritishColumbia", DataType = "Wonga.PublicMessages.Comms.Instructions.IWantToGenerateALegalDocument" )
    , SourceAssembly("Wonga.PublicMessages.Comms.Ca, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IWantToGenerateAPreAuthorisedDebitDocument : MsmqMessage<IWantToGenerateAPreAuthorisedDebitDocument>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
