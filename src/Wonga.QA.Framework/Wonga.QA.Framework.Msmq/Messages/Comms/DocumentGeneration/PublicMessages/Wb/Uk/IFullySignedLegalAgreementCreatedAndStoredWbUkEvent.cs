using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.DocumentGeneration.PublicMessages.Wb.Uk
{
    /// <summary> Wonga.Comms.DocumentGeneration.PublicMessages.Wb.Uk.IFullySignedLegalAgreementCreatedAndStored </summary>
    [XmlRoot("IFullySignedLegalAgreementCreatedAndStored", Namespace = "Wonga.Comms.DocumentGeneration.PublicMessages.Wb.Uk", DataType = "")]
    public partial class IFullySignedLegalAgreementCreatedAndStoredWbUkEvent : MsmqMessage<IFullySignedLegalAgreementCreatedAndStoredWbUkEvent>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid OrganisationId { get; set; }
        public Guid FileId { get; set; }
    }
}