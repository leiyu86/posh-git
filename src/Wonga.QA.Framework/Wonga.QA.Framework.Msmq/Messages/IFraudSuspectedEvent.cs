using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Risk.IFraudSuspected </summary>
    [XmlRoot("IFraudSuspected", Namespace = "Wonga.Risk", DataType = "Wonga.Risk.IRiskEvent")]
    public partial class IFraudSuspectedEvent : MsmqMessage<IFraudSuspectedEvent>
    {
        public Guid AccountId { get; set; }
        public String CaseId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
