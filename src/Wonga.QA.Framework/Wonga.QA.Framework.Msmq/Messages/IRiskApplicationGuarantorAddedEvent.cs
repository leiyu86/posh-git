using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Risk.IRiskApplicationGuarantorAdded </summary>
    [XmlRoot("IRiskApplicationGuarantorAdded", Namespace = "Wonga.Risk", DataType = "Wonga.Risk.IRiskEvent")]
    public partial class IRiskApplicationGuarantorAddedEvent : MsmqMessage<IRiskApplicationGuarantorAddedEvent>
    {
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public String Forename { get; set; }
        public String Surname { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
