using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Comms
{
    [XmlRoot("CreateAlbertaPreApproveDirectDebitMessage", Namespace = "Wonga.Comms.InternalMessages.Ca.Alberta", DataType = "")]
    public partial class CreateAlbertaPreApproveDirectDebitCaCommand : MsmqMessage<CreateAlbertaPreApproveDirectDebitCaCommand>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}