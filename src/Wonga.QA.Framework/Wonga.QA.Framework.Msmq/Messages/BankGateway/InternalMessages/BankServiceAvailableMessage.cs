using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.BankGateway.InternalMessages
{
    /// <summary> Wonga.BankGateway.InternalMessages.BankServiceAvailableMessage </summary>
    [XmlRoot("BankServiceAvailableMessage", Namespace = "Wonga.BankGateway.InternalMessages", DataType = "")]
    public partial class BankServiceAvailableMessage : MsmqMessage<BankServiceAvailableMessage>
    {
        public Int32 BankIntegrationId { get; set; }
        public Int32 TransactionId { get; set; }
        public Int32 DirectDebitId { get; set; }
    }
}