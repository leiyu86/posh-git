using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Payments.InternalMessages.Messages
{
    /// <summary> Wonga.Payments.InternalMessages.Messages.IHardshipDeActivatedInternal </summary>
    [XmlRoot("IHardshipDeActivatedInternal", Namespace = "Wonga.Payments.InternalMessages.Messages", DataType = "Wonga.Payments.PublicMessages.IHardshipDeactivated" )
    , SourceAssembly("Wonga.Payments.InternalMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IHardshipDeActivatedInternal : MsmqMessage<IHardshipDeActivatedInternal>
    {
        public Guid CaseId { get; set; }
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
