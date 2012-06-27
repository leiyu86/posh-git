using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Risk.InternalMessages.Salesforce.business
{
    /// <summary> Wonga.Risk.InternalMessages.Salesforce.business.GeneralManualVerificationInternalRequest </summary>
    [XmlRoot("GeneralManualVerificationInternalRequest", Namespace = "Wonga.Risk.InternalMessages.Salesforce.business", DataType = "Wonga.Risk.InternalMessages.Salesforce.NeedManualVerificationMessage,Wonga.Risk.BaseSagaMessage,NServiceBus.Saga.ISagaMessage")]
    public partial class GeneralManualVerificationInternalRequestCommand : MsmqMessage<GeneralManualVerificationInternalRequestCommand>
    {
        public Guid OrganisationId { get; set; }
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public String Description { get; set; }
        public Guid SagaId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}