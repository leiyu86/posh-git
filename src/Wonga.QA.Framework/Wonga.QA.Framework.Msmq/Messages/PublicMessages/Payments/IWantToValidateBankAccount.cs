using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.PublicMessages.Payments
{
    /// <summary> Wonga.PublicMessages.Payments.IWantToValidateBankAccount </summary>
    [XmlRoot("IWantToValidateBankAccount", Namespace = "Wonga.PublicMessages.Payments", DataType = "" )
    , SourceAssembly("Wonga.PublicMessages.Payments, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IWantToValidateBankAccount : MsmqMessage<IWantToValidateBankAccount>
    {
        public Guid BankAccountId { get; set; }
    }
}
