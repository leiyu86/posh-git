using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Comms
{
    [XmlRoot("IRepaymentArrangementPartiallyRepaidEarlyEmailCompiled", Namespace = "Wonga.Comms.PublicMessages.Za", DataType = "Wonga.Comms.PublicMessages.Za.IEmailCompiled")]
    public partial class IRepaymentArrangementPartiallyRepaidEarlyEmailCompiledZaEvent : MsmqMessage<IRepaymentArrangementPartiallyRepaidEarlyEmailCompiledZaEvent>
    {
        public Guid AccountId { get; set; }
        public Guid HtmlFileId { get; set; }
        public Guid PlainFileId { get; set; }
    }
}