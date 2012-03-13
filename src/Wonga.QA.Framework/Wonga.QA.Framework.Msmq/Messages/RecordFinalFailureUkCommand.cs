using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.BankGateway.InternalMessages.HSBC.Uk.FileWatcherMessages.RecordFinalFailureMessage </summary>
    [XmlRoot("RecordFinalFailureMessage", Namespace = "Wonga.BankGateway.InternalMessages.HSBC.Uk.FileWatcherMessages", DataType = "")]
    public partial class RecordFinalFailureUkCommand : MsmqMessage<RecordFinalFailureUkCommand>
    {
        public String TransactionReference { get; set; }
        public String ErrorCode { get; set; }
    }
}
