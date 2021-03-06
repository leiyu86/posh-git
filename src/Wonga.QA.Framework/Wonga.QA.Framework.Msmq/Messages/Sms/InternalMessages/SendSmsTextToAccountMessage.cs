using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Sms.InternalMessages
{
    /// <summary> Wonga.Sms.InternalMessages.SendSmsTextToAccountMessage </summary>
    [XmlRoot("SendSmsTextToAccountMessage", Namespace = "Wonga.Sms.InternalMessages", DataType = "" )
    , SourceAssembly("Wonga.Sms.InternalMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class SendSmsTextToAccountMessage : MsmqMessage<SendSmsTextToAccountMessage>
    {
        public Guid SmsMessageId { get; set; }
        public Guid AccountId { get; set; }
        public String SmsText { get; set; }
    }
}
