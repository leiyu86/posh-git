using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Risk
{
    [XmlRoot("IBusinessOrganisationPrepared", Namespace = "Wonga.Risk.Business", DataType = "")]
    public partial class IBusinessOrganisationPreparedEvent : MsmqMessage<IBusinessOrganisationPreparedEvent>
    {
        public Guid OrganisationId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}