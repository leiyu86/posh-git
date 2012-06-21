using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq
{
    /// <summary> Wonga.Risk.RiskAddBankAccount </summary>
    [XmlRoot("RiskAddBankAccount", Namespace = "Wonga.Risk", DataType = "")]
    public partial class RiskAddBankAccountCommand : MsmqMessage<RiskAddBankAccountCommand>
    {
        public Guid AccountId { get; set; }
        public Guid BankAccountId { get; set; }
        public String BankName { get; set; }
        public String AccountNumber { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}
