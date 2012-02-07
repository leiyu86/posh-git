using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Comms
{
    [XmlRoot("CreateDocsForFixedTermAppMessage", Namespace = "Wonga.Comms.InternalMessages", DataType = "")]
    public partial class CreateDocsForFixedTermAppCommand : MsmqMessage<CreateDocsForFixedTermAppCommand>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
