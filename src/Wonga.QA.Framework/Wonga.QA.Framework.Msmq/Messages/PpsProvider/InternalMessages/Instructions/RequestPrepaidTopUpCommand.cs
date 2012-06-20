using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Wonga.QA.Framework.Msmq.Enums.Payments.Ca;

namespace Wonga.QA.Framework.Msmq.Messages.PpsProvider.InternalMessages.Instructions
{
    /// <summary> Wonga.PpsProvider.InternalMessages.Instructions.RequestPrepaidTopUp </summary>
    [XmlRoot("RequestPrepaidTopUp", Namespace = "Wonga.PpsProvider.InternalMessages.Instructions", DataType = "Wonga.PublicMessages.Payments.PrepaidCard.IRequestPrepaidTopUp")]
    public partial class RequestPrepaidTopUpCommand : MsmqMessage<RequestPrepaidTopUpCommand>
    {
        public Guid CustomerExternalId { get; set; }
        public Decimal Amount { get; set; }
        public CurrencyCodeIso4217Enum CurrencyCode { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
