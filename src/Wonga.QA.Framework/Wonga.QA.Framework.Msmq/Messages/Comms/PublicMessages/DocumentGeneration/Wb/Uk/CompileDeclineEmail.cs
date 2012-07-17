using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.PublicMessages.DocumentGeneration.Wb.Uk
{
    /// <summary> Wonga.Comms.PublicMessages.DocumentGeneration.Wb.Uk.CompileDeclineEmail </summary>
    [XmlRoot("CompileDeclineEmail", Namespace = "Wonga.Comms.PublicMessages.DocumentGeneration.Wb.Uk", DataType = "")]
    public partial class CompileDeclineEmail : MsmqMessage<CompileDeclineEmail>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid OrganisationId { get; set; }
    }
}