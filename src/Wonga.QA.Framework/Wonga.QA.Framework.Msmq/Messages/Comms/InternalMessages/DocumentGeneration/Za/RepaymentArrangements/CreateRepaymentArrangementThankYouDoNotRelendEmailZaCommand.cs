using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.DocumentGeneration.Za.RepaymentArrangements
{
    /// <summary> Wonga.Comms.InternalMessages.DocumentGeneration.Za.RepaymentArrangements.CreateRepaymentArrangementThankYouDoNotRelendEmailMessage </summary>
    [XmlRoot("CreateRepaymentArrangementThankYouDoNotRelendEmailMessage", Namespace = "Wonga.Comms.InternalMessages.DocumentGeneration.Za.RepaymentArrangements", DataType = "")]
    public partial class CreateRepaymentArrangementThankYouDoNotRelendEmailZaCommand : MsmqMessage<CreateRepaymentArrangementThankYouDoNotRelendEmailZaCommand>
    {
        public Guid AccountId { get; set; }
        public Guid RepaymentArrangementId { get; set; }
    }
}