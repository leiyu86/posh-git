using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Comms.PublicMessages.Wb.Uk.ILoanAgreementAddedToLegalRepository </summary>
    [XmlRoot("ILoanAgreementAddedToLegalRepository", Namespace = "Wonga.Comms.PublicMessages.Wb.Uk", DataType = "")]
    public partial class ILoanAgreementAddedToLegalRepositoryWbUkEvent : MsmqMessage<ILoanAgreementAddedToLegalRepositoryWbUkEvent>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid ExternalId { get; set; }
        public Guid OrganisationId { get; set; }
    }
}
