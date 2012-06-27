using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Wonga.QA.Framework.Msmq.Enums.Integration.Comms.Enums;

namespace Wonga.QA.Framework.Msmq.Messages.PublicMessages.Comms.Instructions
{
    /// <summary> Wonga.PublicMessages.Comms.Instructions.IWantToSendArrearsCommunicationSmsResponse </summary>
    [XmlRoot("IWantToSendArrearsCommunicationSmsResponse", Namespace = "Wonga.PublicMessages.Comms.Instructions", DataType = "")]
    public partial class IWantToSendArrearsCommunicationSmsResponseEvent : MsmqMessage<IWantToSendArrearsCommunicationSmsResponseEvent>
    {
        public Guid ArrearsCommunicationId { get; set; }
        public ArrearsSmsCommunicationEnum Type { get; set; }
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Boolean Successful { get; set; }
    }
}