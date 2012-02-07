using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Risk
{
    [XmlRoot("EidDecisionResponseMessage", Namespace = "Wonga.Risk.InternalMessages.Equifax", DataType = "Wonga.Risk.InternalMessages.Equifax.EidBaseResponseMessage,Wonga.Risk.BaseSagaMessage,NServiceBus.Saga.ISagaMessage")]
    public partial class EidDecisionResponseCaCommand : MsmqMessage<EidDecisionResponseCaCommand>
    {
        public String[] ReasonCodes { get; set; }
        public Int32 Score { get; set; }
        public String Decision { get; set; }
        public Object RiskComponents { get; set; }
        public String TransactionKey { get; set; }
        public Guid SagaId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
