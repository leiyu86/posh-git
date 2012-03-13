using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Risk.Iovation.IovationResultAddedMessage </summary>
    [XmlRoot("IovationResultAddedMessage", Namespace = "Wonga.Risk.Iovation", DataType = "Wonga.Risk.BaseSagaMessage,NServiceBus.Saga.ISagaMessage,Wonga.Risk.IResumeRiskWorkflow")]
    public partial class IovationResultAddedCommand : MsmqMessage<IovationResultAddedCommand>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public IovationAdviceEnum Result { get; set; }
        public String Reason { get; set; }
        public Object Details { get; set; }
        public String TrackingNumber { get; set; }
        public Guid SagaId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
