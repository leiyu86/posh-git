using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Comms.InternalMessages.DocumentGeneration.Za.RepaymentArrangements.CreateRepaymentArrangementThankYouEmailCompleteMessage </summary>
    [XmlRoot("CreateRepaymentArrangementThankYouEmailCompleteMessage", Namespace = "Wonga.Comms.InternalMessages.DocumentGeneration.Za.RepaymentArrangements", DataType = "")]
    public partial class CreateRepaymentArrangementThankYouEmailCompleteZaCommand : MsmqMessage<CreateRepaymentArrangementThankYouEmailCompleteZaCommand>
    {
        public Guid AccountId { get; set; }
        public Byte[] HtmlContent { get; set; }
        public Byte[] PlainContent { get; set; }
    }
}