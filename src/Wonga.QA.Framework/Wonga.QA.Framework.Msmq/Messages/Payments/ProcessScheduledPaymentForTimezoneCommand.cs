using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Payments
{
    [XmlRoot("ProcessScheduledPaymentForTimezone", Namespace = "Wonga.Payments.InternalMessages.Messages", DataType = "")]
    public partial class ProcessScheduledPaymentForTimezoneCommand : MsmqMessage<ProcessScheduledPaymentForTimezoneCommand>
    {
        public MsTimeZoneEnum TimeZone { get; set; }
        public Guid RequestId { get; set; }
    }
}