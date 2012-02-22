using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Comms
{
    [XmlRoot("RecordActivityWhenEmailSendIsCompletedMessage", Namespace = "Wonga.Comms.InternalMessages.Za", DataType = "")]
    public partial class RecordActivityWhenEmailSendIsCompletedZaCommand : MsmqMessage<RecordActivityWhenEmailSendIsCompletedZaCommand>
    {
        public Guid ActivityId { get; set; }
        public Guid AccountId { get; set; }
        public String Subject { get; set; }
    }
}