using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Risk.PLater.IPayLaterApplicationAccepted </summary>
    [XmlRoot("IPayLaterApplicationAccepted", Namespace = "Wonga.Risk.PLater", DataType = "Wonga.Risk.IApplicationAccepted,Wonga.Risk.IAcceptedDecision,Wonga.Risk.IDecisionMessage,Wonga.Risk.IRiskEvent")]
    public partial class IPayLaterApplicationAcceptedEvent : MsmqMessage<IPayLaterApplicationAcceptedEvent>
    {
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
