using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Payments.Csapi.Commands
{
    /// <summary> Wonga.Payments.Csapi.Commands.AddPersonalPaymentCard </summary>
    [XmlRoot("AddPersonalPaymentCard", Namespace = "Wonga.Payments.Csapi.Commands", DataType = "" )
    , SourceAssembly("Wonga.Payments.Csapi.Commands, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class AddPersonalPaymentCard : MsmqMessage<AddPersonalPaymentCard>
    {
        public Guid AccountId { get; set; }
        public String CardType { get; set; }
        public String Number { get; set; }
        public String HolderName { get; set; }
        public String IssueNo { get; set; }
        public String SecurityCode { get; set; }
        public Boolean IsCreditCard { get; set; }
        public Boolean IsPrimary { get; set; }
        public String StartDateXml { get; set; }
        public String ExpiryDateXml { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}
