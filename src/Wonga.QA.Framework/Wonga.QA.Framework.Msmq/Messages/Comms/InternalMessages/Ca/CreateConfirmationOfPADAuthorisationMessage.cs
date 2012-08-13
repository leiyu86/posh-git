using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.InternalMessages.Ca
{
    /// <summary> Wonga.Comms.InternalMessages.Ca.CreateConfirmationOfPADAuthorisationMessage </summary>
    [XmlRoot("CreateConfirmationOfPADAuthorisationMessage", Namespace = "Wonga.Comms.InternalMessages.Ca", DataType = "" )
    , SourceAssembly("Wonga.Comms.InternalMessages.Ca, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class CreateConfirmationOfPADAuthorisationMessage : MsmqMessage<CreateConfirmationOfPADAuthorisationMessage>
    {
        public Guid AccountId { get; set; }
        public Guid ApplicationId { get; set; }
    }
}
