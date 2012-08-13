using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Payments.PublicMessages
{
    /// <summary> Wonga.Payments.PublicMessages.IPayLaterApplicationCanceled </summary>
    [XmlRoot("IPayLaterApplicationCanceled", Namespace = "Wonga.Payments.PublicMessages", DataType = "Wonga.Payments.PublicMessages.IApplicationCanceled,Wonga.Payments.PublicMessages.IPaymentsEvent" )
    , SourceAssembly("Wonga.Payments.PublicMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IPayLaterApplicationCanceled : MsmqMessage<IPayLaterApplicationCanceled>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
