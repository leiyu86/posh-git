using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.PublicMessages.Comms.Ca.Instructions.BritishColumbia
{
    /// <summary> Wonga.PublicMessages.Comms.Ca.Instructions.BritishColumbia.IWantToGenerateAConfirmationOfPreAuthorisedDebitDocument </summary>
    [XmlRoot("IWantToGenerateAConfirmationOfPreAuthorisedDebitDocument", Namespace = "Wonga.PublicMessages.Comms.Ca.Instructions.BritishColumbia", DataType = "Wonga.PublicMessages.Comms.Instructions.IWantToGenerateALegalDocument")]
    public partial class IWantToGenerateAConfirmationOfPreAuthorisedDebitDocumentCaEvent : MsmqMessage<IWantToGenerateAConfirmationOfPreAuthorisedDebitDocumentCaEvent>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}