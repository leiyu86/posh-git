using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    /// <summary> Wonga.Payments.Queries.GetRepaymentArrangementAllowed </summary>
    [XmlRoot("GetRepaymentArrangementAllowed")]
    public partial class GetRepaymentArrangementAllowedQuery : ApiRequest<GetRepaymentArrangementAllowedQuery>
    {
        public Object ApplicationId { get; set; }
    }
}
