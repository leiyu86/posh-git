using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Risk
{
    [XmlRoot("IAcceptedDecision", Namespace = "Wonga.Risk", DataType = "Wonga.Risk.IApplicationDecision")]
    public partial class IAcceptedDecisionEvent : MsmqMessage<IAcceptedDecisionEvent>
    {
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
    }
}