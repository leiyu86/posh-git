using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Bi
{
    [XmlRoot("CalcCollectionDateMessage", Namespace = "Wonga.Bi.Messages", DataType = "")]
    public partial class CalcCollectionDateCommand : MsmqMessage<CalcCollectionDateCommand>
    {
        public Guid AccountId { get; set; }
        public Guid? ApplicationId { get; set; }
        public DateTime PromiseDate { get; set; }
    }
}
