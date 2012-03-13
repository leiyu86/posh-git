using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Risk.IWorkflowDeclined </summary>
    [XmlRoot("IWorkflowDeclined", Namespace = "Wonga.Risk", DataType = "Wonga.Risk.IDeclinedDecision,Wonga.Risk.IDecisionMessage,Wonga.Risk.IRiskEvent,Wonga.Risk.IWorkflowDecision")]
    public partial class IWorkflowDeclinedEvent : MsmqMessage<IWorkflowDeclinedEvent>
    {
        public String FailedCheckpointName { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
