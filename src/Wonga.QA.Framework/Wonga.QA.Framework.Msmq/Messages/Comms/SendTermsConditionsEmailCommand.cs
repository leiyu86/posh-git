using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Comms
{
    [XmlRoot("SendTermsConditionsEmailMessage", Namespace = "Wonga.Comms.InternalMessages.Email", DataType = "")]
    public partial class SendTermsConditionsEmailCommand : MsmqMessage<SendTermsConditionsEmailCommand>
    {
        public Guid AccountId { get; set; }
        public String Email { get; set; }
    }
}
