using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Risk.InternalMessages.Salesforce
{
    /// <summary> Wonga.Risk.InternalMessages.Salesforce.CIFASFlagResponseMessage </summary>
    [XmlRoot("CIFASFlagResponseMessage", Namespace = "Wonga.Risk.InternalMessages.Salesforce", DataType = "Wonga.Risk.InternalMessages.Salesforce.ManualVerificationResponseMessage,Wonga.Risk.BaseSagaMessage,NServiceBus.Saga.ISagaMessage,Wonga.Risk.IResumeRiskWorkflow" )
    , SourceAssembly("Wonga.Risk.InternalMessages.Salesforce, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class CIFASFlagResponseMessage : MsmqMessage<CIFASFlagResponseMessage>
    {
        public Int32? Probability { get; set; }
        public Guid SagaId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
