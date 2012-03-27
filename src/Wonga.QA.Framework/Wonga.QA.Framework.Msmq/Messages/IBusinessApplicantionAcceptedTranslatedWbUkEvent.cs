using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Comms.InternalMessages.Events.Wb.Uk.IBusinessApplicantionAcceptedTranslated </summary>
    [XmlRoot("IBusinessApplicantionAcceptedTranslated", Namespace = "Wonga.Comms.InternalMessages.Events.Wb.Uk", DataType = "Wonga.Comms.PublicMessages.ICommsEvent")]
    public partial class IBusinessApplicantionAcceptedTranslatedWbUkEvent : MsmqMessage<IBusinessApplicantionAcceptedTranslatedWbUkEvent>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid OrganisationId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}