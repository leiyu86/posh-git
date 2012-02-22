using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Comms
{
    [XmlRoot("CompleteMobilePhoneVerificationMessage", Namespace = "Wonga.Comms.Commands", DataType = "")]
    public partial class CompleteMobilePhoneVerificationCommand : MsmqMessage<CompleteMobilePhoneVerificationCommand>
    {
        public Guid VerificationId { get; set; }
        public String Pin { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}