using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Risk
{
    [XmlRoot("IBusinessBankAccountAddedToApplication", Namespace = "Wonga.Risk.Business", DataType = "")]
    public partial class IBusinessBankAccountAddedToApplicationEvent : MsmqMessage<IBusinessBankAccountAddedToApplicationEvent>
    {
        public Guid OrganisationId { get; set; }
        public Guid ApplicationId { get; set; }
        public Guid BusinessBankAccountId { get; set; }
    }
}
