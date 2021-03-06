using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Cs.Requests.Payments.Csapi.Queries
{
    /// <summary> Wonga.Payments.Csapi.Queries.GetTransactions </summary>
    [XmlRoot("GetTransactions")]
    public partial class GetTransactionsQuery : CsRequest<GetTransactionsQuery>
    {
        public Object ApplicationGuid { get; set; }
    }
}
