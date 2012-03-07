using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    /// <summary> Wonga.Comms.Queries.GetLoanAgreement </summary>
    [XmlRoot("GetLoanAgreement")]
    public partial class GetLoanAgreementQuery : ApiRequest<GetLoanAgreementQuery>
    {
        public Object ApplicationId { get; set; }
    }
}
