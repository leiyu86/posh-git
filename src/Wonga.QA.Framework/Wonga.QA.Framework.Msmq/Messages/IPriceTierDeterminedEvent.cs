using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Risk.IPriceTierDetermined </summary>
    [XmlRoot("IPriceTierDetermined", Namespace = "Wonga.Risk", DataType = "Wonga.Risk.IRiskEvent")]
    public partial class IPriceTierDeterminedEvent : MsmqMessage<IPriceTierDeterminedEvent>
    {
        public Guid ApplicationId { get; set; }
        public UInt32 Tier { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
