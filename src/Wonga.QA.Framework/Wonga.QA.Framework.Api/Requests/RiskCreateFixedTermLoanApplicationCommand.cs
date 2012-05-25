using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    /// <summary> Wonga.Risk.Commands.RiskCreateFixedTermLoanApplication </summary>
    [XmlRoot("RiskCreateFixedTermLoanApplication")]
    public partial class RiskCreateFixedTermLoanApplicationCommand : ApiRequest<RiskCreateFixedTermLoanApplicationCommand>
    {
        public Object AccountId { get; set; }
        public Object ApplicationId { get; set; }
        public Object PaymentCardId { get; set; }
        public Object BankAccountId { get; set; }
        public Object Currency { get; set; }
        public Object PromiseDate { get; set; }
        public Object LoanAmount { get; set; }
    }
}
