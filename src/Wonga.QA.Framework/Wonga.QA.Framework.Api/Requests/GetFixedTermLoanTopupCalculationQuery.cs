using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    [XmlRoot("GetFixedTermLoanTopupCalculation")]
    public partial class GetFixedTermLoanTopupCalculationQuery : ApiRequest<GetFixedTermLoanTopupCalculationQuery>
    {
        public Object ApplicationId { get; set; }
        public Object TopupAmount { get; set; }
    }
}