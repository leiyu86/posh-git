using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    /// <summary> Wonga.Payments.Queries.Ca.GetInstitutionBranchCodeIsValid </summary>
    [XmlRoot("GetInstitutionBranchCodeIsValid")]
    public partial class GetInstitutionBranchCodeIsValidCaQuery : ApiRequest<GetInstitutionBranchCodeIsValidCaQuery>
    {
        public Object InstitutionNumber { get; set; }
        public Object BranchCode { get; set; }
    }
}
