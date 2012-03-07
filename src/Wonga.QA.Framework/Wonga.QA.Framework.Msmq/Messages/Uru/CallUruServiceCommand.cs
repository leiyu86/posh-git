using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Uru
{
    /// <summary> Wonga.Uru.InternalMessages.CallUruServiceMessage </summary>
    [XmlRoot("CallUruServiceMessage", Namespace = "Wonga.Uru.InternalMessages", DataType = "NServiceBus.Saga.ISagaMessage")]
    public partial class CallUruServiceCommand : MsmqMessage<CallUruServiceCommand>
    {
        public Guid SagaId { get; set; }
        public Guid AccountId { get; set; }
        public Object UruDataInput { get; set; }
    }
}
