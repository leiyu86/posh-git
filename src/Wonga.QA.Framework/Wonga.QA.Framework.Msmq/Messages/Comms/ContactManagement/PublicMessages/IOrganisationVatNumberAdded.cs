using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.ContactManagement.PublicMessages
{
    /// <summary> Wonga.Comms.ContactManagement.PublicMessages.IOrganisationVatNumberAdded </summary>
    [XmlRoot("IOrganisationVatNumberAdded", Namespace = "Wonga.Comms.ContactManagement.PublicMessages", DataType = "Wonga.Comms.ContactManagement.PublicMessages.ICommsEvent" )
    , SourceAssembly("Wonga.Comms.ContactManagement.PublicMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IOrganisationVatNumberAdded : MsmqMessage<IOrganisationVatNumberAdded>
    {
        public Guid OrganisationId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
