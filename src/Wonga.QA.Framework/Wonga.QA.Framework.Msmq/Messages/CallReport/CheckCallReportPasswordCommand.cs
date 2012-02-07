using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.CallReport
{
    [XmlRoot("CheckCallReportPassword", Namespace = "Wonga.CallReport.Handlers.InternalMessages", DataType = "NServiceBus.Saga.ISagaMessage")]
    public partial class CheckCallReportPasswordCommand : MsmqMessage<CheckCallReportPasswordCommand>
    {
        public Guid SagaId { get; set; }
    }
}
