using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Payments
{
    /// <summary> Wonga.Payments.InternalMessages.SagaMessages.BaseSagaMessage </summary>
    [XmlRoot("BaseSagaMessage", Namespace = "Wonga.Payments.InternalMessages.SagaMessages", DataType = "NServiceBus.Saga.ISagaMessage")]
    public partial class BaseSagaCommand : MsmqMessage<BaseSagaCommand>
    {
        public Guid SagaId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
