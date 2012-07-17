using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.Events.Ca
{
    /// <summary> Wonga.Comms.InternalMessages.Events.Ca.IConfirmationOfPADAuthorisationProducedInternal </summary>
    [XmlRoot("IConfirmationOfPADAuthorisationProducedInternal", Namespace = "Wonga.Comms.InternalMessages.Events.Ca", DataType = "Wonga.Comms.PublicMessages.Ca.IConfirmationOfPADAuthorisationProduced")]
    public partial class IConfirmationOfPADAuthorisationProducedInternal : MsmqMessage<IConfirmationOfPADAuthorisationProducedInternal>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid FileStorageId { get; set; }
    }
}