using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Cs
{
    /// <summary> Wonga.Payments.Csapi.Queries.CsGetBankAccounts </summary>
    [XmlRoot("CsGetBankAccounts")]
    public partial class CsGetBankAccountsQuery : CsRequest<CsGetBankAccountsQuery>
    {
        public Object AccountId { get; set; }
    }
}
