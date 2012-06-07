using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.PublicMessages.Comms.Instructions.IWantToSendScheduledPingFailedEmail </summary>
    [XmlRoot("IWantToSendScheduledPingFailedEmail", Namespace = "Wonga.PublicMessages.Comms.Instructions", DataType = "")]
    public partial class IWantToSendScheduledPingFailedEmailEvent : MsmqMessage<IWantToSendScheduledPingFailedEmailEvent>
    {
        public Guid AccountId { get; set; }
        public Guid FileId { get; set; }
        public Guid OriginatingSagaId { get; set; }
    }
}