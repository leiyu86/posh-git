using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Risk
{
    /// <summary> Wonga.Risk.IApplicationDecision </summary>
    [XmlRoot("IApplicationDecision", Namespace = "Wonga.Risk", DataType = "Wonga.Risk.IRiskEvent" )
    , SourceAssembly("Wonga.Risk.PublicMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IApplicationDecision : MsmqMessage<IApplicationDecision>
    {
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
