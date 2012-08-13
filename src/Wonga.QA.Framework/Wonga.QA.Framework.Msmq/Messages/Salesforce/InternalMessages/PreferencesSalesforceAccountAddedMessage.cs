using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Salesforce.InternalMessages
{
    /// <summary> Wonga.Salesforce.InternalMessages.PreferencesSalesforceAccountAddedMessage </summary>
    [XmlRoot("PreferencesSalesforceAccountAddedMessage", Namespace = "Wonga.Salesforce.InternalMessages", DataType = "" )
    , SourceAssembly("Wonga.Salesforce.InternalMessages, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class PreferencesSalesforceAccountAddedMessage : MsmqMessage<PreferencesSalesforceAccountAddedMessage>
    {
        public Guid AccountId { get; set; }
    }
}
