using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Risk
{
    [XmlRoot("CalculateCreditLimitMessage", Namespace = "Wonga.Risk", DataType = "")]
    public partial class CalculateCreditLimitCommand : MsmqMessage<CalculateCreditLimitCommand>
    {
        public Guid AccountId { get; set; }
        public Guid? ApplicationId { get; set; }
    }
}