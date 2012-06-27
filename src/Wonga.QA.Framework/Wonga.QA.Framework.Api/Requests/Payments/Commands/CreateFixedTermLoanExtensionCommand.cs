using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api.Requests.Payments.Commands
{
    /// <summary> Wonga.Payments.Commands.CreateFixedTermLoanExtension </summary>
    [XmlRoot("CreateFixedTermLoanExtension")]
    public partial class CreateFixedTermLoanExtensionCommand : ApiRequest<CreateFixedTermLoanExtensionCommand>
    {
        public Object ApplicationId { get; set; }
        public Object ExtensionId { get; set; }
        public Object ExtendDate { get; set; }
        public Object PaymentCardId { get; set; }
        public Object PaymentCardCv2 { get; set; }
        public Object PartPaymentAmount { get; set; }
    }
}