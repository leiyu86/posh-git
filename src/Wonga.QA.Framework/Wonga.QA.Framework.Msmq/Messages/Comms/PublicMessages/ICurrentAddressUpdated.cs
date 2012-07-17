using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.PublicMessages
{
    /// <summary> Wonga.Comms.PublicMessages.ICurrentAddressUpdated </summary>
    [XmlRoot("ICurrentAddressUpdated", Namespace = "Wonga.Comms.PublicMessages", DataType = "")]
    public partial class ICurrentAddressUpdated : MsmqMessage<ICurrentAddressUpdated>
    {
        public Guid AccountId { get; set; }
    }
}