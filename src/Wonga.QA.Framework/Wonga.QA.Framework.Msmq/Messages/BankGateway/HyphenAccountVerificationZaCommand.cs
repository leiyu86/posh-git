using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.BankGateway
{
    /// <summary> Wonga.BankGateway.InternalMessages.Hyphen.Za.HyphenAccountVerificationMessage </summary>
    [XmlRoot("HyphenAccountVerificationMessage", Namespace = "Wonga.BankGateway.InternalMessages.Hyphen.Za", DataType = "")]
    public partial class HyphenAccountVerificationZaCommand : MsmqMessage<HyphenAccountVerificationZaCommand>
    {
        public Int32 BankAccountVerificationId { get; set; }
    }
}
