using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Comms
{
    [XmlRoot("CreateAndStoreFullySignedPersonalGuaranteeDocumentMessage", Namespace = "Wonga.Comms.InternalMessages.DocumentGeneration.Wb.Uk.PersonalGuarantee", DataType = "")]
    public partial class CreateAndStoreFullySignedPersonalGuaranteeDocumentWbUkCommand : MsmqMessage<CreateAndStoreFullySignedPersonalGuaranteeDocumentWbUkCommand>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid OrganisationId { get; set; }
    }
}
