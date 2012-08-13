using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.Events.Wb.Uk
{
    /// <summary> Wonga.Comms.InternalMessages.Events.Wb.Uk.IFullySignedLegalAgreementCreatedAndStoredTranslated </summary>
    [XmlRoot("IFullySignedLegalAgreementCreatedAndStoredTranslated", Namespace = "Wonga.Comms.InternalMessages.Events.Wb.Uk", DataType = "Wonga.Comms.PublicMessages.ICommsEvent" )
    , SourceAssembly("Wonga.Comms.InternalMessages.Events.Wb.Uk, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IFullySignedLegalAgreementCreatedAndStoredTranslated : MsmqMessage<IFullySignedLegalAgreementCreatedAndStoredTranslated>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid OrganisationId { get; set; }
        public Guid FileId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
