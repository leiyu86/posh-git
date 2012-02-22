using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Risk
{
    [XmlRoot("SubmitClientWatermarkCommand", Namespace = "Wonga.Risk", DataType = "")]
    public partial class SubmitClientWatermarkCommand : MsmqMessage<SubmitClientWatermarkCommand>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public String ClientIPAddress { get; set; }
        public String BlackboxData { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}