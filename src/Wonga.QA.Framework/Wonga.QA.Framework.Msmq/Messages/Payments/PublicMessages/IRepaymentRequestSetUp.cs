using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Payments.PublicMessages
{
    /// <summary> Wonga.Payments.PublicMessages.IRepaymentRequestSetUp </summary>
    [XmlRoot("IRepaymentRequestSetUp", Namespace = "Wonga.Payments.PublicMessages", DataType = "" )
    , SourceAssembly("Wonga.Payments.PublicMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IRepaymentRequestSetUp : MsmqMessage<IRepaymentRequestSetUp>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid RepaymentRequestId { get; set; }
    }
}
