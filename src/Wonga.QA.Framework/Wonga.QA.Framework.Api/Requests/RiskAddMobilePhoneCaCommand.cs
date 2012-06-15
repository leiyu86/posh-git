using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    /// <summary> Wonga.Risk.Commands.Ca.RiskAddMobilePhone </summary>
    [XmlRoot("RiskAddMobilePhone")]
    public partial class RiskAddMobilePhoneCaCommand : ApiRequest<RiskAddMobilePhoneCaCommand>
    {
        public Object AccountId { get; set; }
        public Object MobilePhone { get; set; }
    }
}
