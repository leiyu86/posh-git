using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.Suppressions
{
    /// <summary> Wonga.Comms.InternalMessages.Suppressions.StartBankruptcySuppressionMessage </summary>
    [XmlRoot("StartBankruptcySuppressionMessage", Namespace = "Wonga.Comms.InternalMessages.Suppressions", DataType = "")]
    public partial class StartBankruptcySuppressionMessage : MsmqMessage<StartBankruptcySuppressionMessage>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}