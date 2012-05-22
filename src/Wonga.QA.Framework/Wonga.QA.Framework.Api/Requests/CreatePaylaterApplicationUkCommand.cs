using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    /// <summary> Wonga.Payments.Commands.PLater.Uk.CreatePaylaterApplication </summary>
    [XmlRoot("CreatePaylaterApplication")]
    public partial class CreatePaylaterApplicationUkCommand : ApiRequest<CreatePaylaterApplicationUkCommand>
    {
        public Object AccountId { get; set; }
        public Object ApplicationId { get; set; }
        public Object MerchantId { get; set; }
        public Object TotalAmount { get; set; }
    }
}
