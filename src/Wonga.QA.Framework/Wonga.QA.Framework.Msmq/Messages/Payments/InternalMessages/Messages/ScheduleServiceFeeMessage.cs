using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Payments.InternalMessages.Messages
{
    /// <summary> Wonga.Payments.InternalMessages.Messages.ScheduleServiceFeeMessage </summary>
    [XmlRoot("ScheduleServiceFeeMessage", Namespace = "Wonga.Payments.InternalMessages.Messages", DataType = "" )
    , SourceAssembly("Wonga.Payments.InternalMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class ScheduleServiceFeeMessage : MsmqMessage<ScheduleServiceFeeMessage>
    {
        public Int32 ApplicationId { get; set; }
        public DateTime FirstServiceFeeDate { get; set; }
    }
}
