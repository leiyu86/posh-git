using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Email
{
    [XmlRoot("IRepaymentArrangementThankYouDoNotRelendEmailSent", Namespace = "Wonga.Email.PublicMessages.Za", DataType = "Wonga.Email.PublicMessages.Za.IEmailSent")]
    public partial class IRepaymentArrangementThankYouDoNotRelendEmailSentZaEvent : MsmqMessage<IRepaymentArrangementThankYouDoNotRelendEmailSentZaEvent>
    {
        public Guid AccountId { get; set; }
    }
}