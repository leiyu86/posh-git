using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Comms.Csapi.Commands.CsRemoveComplaint </summary>
    [XmlRoot("CsRemoveComplaint", Namespace = "Wonga.Comms.Csapi.Commands", DataType = "")]
    public partial class CsRemoveComplaintCsCommand : MsmqMessage<CsRemoveComplaintCsCommand>
    {
        public Guid ApplicationId { get; set; }
        public Guid AccountId { get; set; }
        public Guid CaseId { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}