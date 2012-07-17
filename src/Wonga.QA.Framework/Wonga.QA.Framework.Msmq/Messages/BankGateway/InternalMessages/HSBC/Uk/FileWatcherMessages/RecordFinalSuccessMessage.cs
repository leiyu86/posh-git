using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.BankGateway.InternalMessages.HSBC.Uk.FileWatcherMessages
{
    /// <summary> Wonga.BankGateway.InternalMessages.HSBC.Uk.FileWatcherMessages.RecordFinalSuccessMessage </summary>
    [XmlRoot("RecordFinalSuccessMessage", Namespace = "Wonga.BankGateway.InternalMessages.HSBC.Uk.FileWatcherMessages", DataType = "")]
    public partial class RecordFinalSuccessMessage : MsmqMessage<RecordFinalSuccessMessage>
    {
        public Int32 TransactionId { get; set; }
    }
}