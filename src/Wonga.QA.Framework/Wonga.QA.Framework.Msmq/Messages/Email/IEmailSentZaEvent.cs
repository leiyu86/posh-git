using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Email
{
    [XmlRoot("IEmailSent", Namespace = "Wonga.Email.PublicMessages.Za", DataType = "")]
    public partial class IEmailSentZaEvent : MsmqMessage<IEmailSentZaEvent>
    {
        public Guid AccountId { get; set; }
    }
}