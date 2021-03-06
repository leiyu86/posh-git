using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.Messages.Ca.Alberta
{
    /// <summary> Wonga.Comms.Messages.Ca.Alberta.CreateLoanDocumentsForAb </summary>
    [XmlRoot("CreateLoanDocumentsForAb", Namespace = "Wonga.Comms.Messages.Ca.Alberta", DataType = "" )
    , SourceAssembly("Wonga.Comms.Messages.Ca, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class CreateLoanDocumentsForAb : MsmqMessage<CreateLoanDocumentsForAb>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
