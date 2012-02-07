using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    [XmlRoot("GetAccountByToken")]
    public partial class GetAccountByTokenQuery : ApiRequest<GetAccountByTokenQuery>
    {
        public Object Token { get; set; }
    }
}
