using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages
{
    /// <summary> Wonga.Comms.InternalMessages.IPersonalDetailsAddedInternal </summary>
    [XmlRoot("IPersonalDetailsAddedInternal", Namespace = "Wonga.Comms.InternalMessages", DataType = "Wonga.Comms.PublicMessages.IPersonalDetailsAdded,Wonga.Comms.PublicMessages.ICommsEvent")]
    public partial class IPersonalDetailsAddedInternal : MsmqMessage<IPersonalDetailsAddedInternal>
    {
        public Guid AccountId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}