using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Risk
{
    [XmlRoot("CallReportBatchDataRequestMessage", Namespace = "Wonga.Risk.CallReport", DataType = "Wonga.Risk.BaseSagaMessage,NServiceBus.Saga.ISagaMessage")]
    public partial class CallReportBatchDataRequestUkCommand : MsmqMessage<CallReportBatchDataRequestUkCommand>
    {
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public Guid SagaId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}