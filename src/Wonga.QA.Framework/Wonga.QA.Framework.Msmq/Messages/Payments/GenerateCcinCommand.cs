using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Payments
{
    [XmlRoot("GenerateCcinMessage", Namespace = "Wonga.Payments.InternalMessages.Messages", DataType = "")]
    public partial class GenerateCcinCommand : MsmqMessage<GenerateCcinCommand>
    {
        public Guid AccountId { get; set; }
    }
}