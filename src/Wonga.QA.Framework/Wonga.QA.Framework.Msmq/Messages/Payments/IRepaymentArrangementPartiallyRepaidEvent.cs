using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Payments
{
    [XmlRoot("IRepaymentArrangementPartiallyRepaid", Namespace = "Wonga.Payments.PublicMessages", DataType = "")]
    public partial class IRepaymentArrangementPartiallyRepaidEvent : MsmqMessage<IRepaymentArrangementPartiallyRepaidEvent>
    {
        public Boolean IsEarlyPayment { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid RepaymentArrangementId { get; set; }
    }
}