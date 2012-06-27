using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.PublicMessages.Comms.Prepaid.Uk.Instructions
{
    /// <summary> Wonga.PublicMessages.Comms.Prepaid.Uk.Instructions.IWantToSendSuccessfulPremiumCardCreationEmail </summary>
    [XmlRoot("IWantToSendSuccessfulPremiumCardCreationEmail", Namespace = "Wonga.PublicMessages.Comms.Prepaid.Uk.Instructions", DataType = "")]
    public partial class IWantToSendSuccessfulPremiumCardCreationEmailUkEvent : MsmqMessage<IWantToSendSuccessfulPremiumCardCreationEmailUkEvent>
    {
        public Guid DocumentId { get; set; }
        public Guid AccountId { get; set; }
    }
}