using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    [XmlRoot("SignBusinessApplication")]
    public partial class SignBusinessApplicationWbUkCommand : ApiRequest<SignBusinessApplicationWbUkCommand>
    {
        public Object AccountId { get; set; }
        public Object ApplicationId { get; set; }
    }
}
