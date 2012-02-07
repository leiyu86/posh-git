using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Risk
{
    [XmlRoot("IDeclinedDecision", Namespace = "Wonga.Risk", DataType = "Wonga.Risk.IApplicationDecision")]
    public partial class IDeclinedDecisionEvent : MsmqMessage<IDeclinedDecisionEvent>
    {
        public String FailedCheckpointName { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
    }
}
