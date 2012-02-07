using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    [XmlRoot("SendVerificationEmail")]
    public partial class SendVerificationEmailCommand : ApiRequest<SendVerificationEmailCommand>
    {
        public Object AccountId { get; set; }
        public Object Email { get; set; }
        public Object UriFragment { get; set; }
    }
}
