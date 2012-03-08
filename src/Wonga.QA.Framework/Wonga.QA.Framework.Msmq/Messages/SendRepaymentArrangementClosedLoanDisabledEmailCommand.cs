using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Comms.InternalMessages.Email.SendRepaymentArrangementClosedLoanDisabledEmailMessage </summary>
    [XmlRoot("SendRepaymentArrangementClosedLoanDisabledEmailMessage", Namespace = "Wonga.Comms.InternalMessages.Email", DataType = "Wonga.Comms.InternalMessages.Email.BaseSimpleEmailMessage,Wonga.Comms.InternalMessages.SagaMessages.BaseSagaMessage,NServiceBus.Saga.ISagaMessage")]
    public partial class SendRepaymentArrangementClosedLoanDisabledEmailCommand : MsmqMessage<SendRepaymentArrangementClosedLoanDisabledEmailCommand>
    {
        public String FirstName { get; set; }
        public String Email { get; set; }
        public Guid SagaId { get; set; }
    }
}