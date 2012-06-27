using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api.Requests.Marketing.Queries
{
    /// <summary> Wonga.Marketing.Queries.Sample </summary>
    [XmlRoot("Sample")]
    public partial class SampleQuery : ApiRequest<SampleQuery>
    {
        public Object Hello { get; set; }
    }
}