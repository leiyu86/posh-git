using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Payments.PublicMessages
{
    /// <summary> Wonga.Payments.PublicMessages.IHsbcCashOutPpsStartMessage </summary>
    [XmlRoot("IHsbcCashOutPpsStartMessage", Namespace = "Wonga.Payments.PublicMessages", DataType = "" )
    , SourceAssembly("Wonga.Payments.PublicMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class IHsbcCashOutPpsStartMessage : MsmqMessage<IHsbcCashOutPpsStartMessage>
    {
    }
}
