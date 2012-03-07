using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Experian
{
    /// <summary> Wonga.Experian.Handlers.ExperianInternalBureauMessage </summary>
    [XmlRoot("ExperianInternalBureauMessage", Namespace = "Wonga.Experian.Handlers", DataType = "NServiceBus.Saga.ISagaMessage")]
    public partial class ExperianInternalBureauCommand : MsmqMessage<ExperianInternalBureauCommand>
    {
        public Guid SagaId { get; set; }
        public Object Request { get; set; }
    }
}
