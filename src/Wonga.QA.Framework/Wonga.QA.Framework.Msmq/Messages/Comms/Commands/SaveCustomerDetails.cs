using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Wonga.QA.Framework.Msmq.Enums.Integration.Comms.Enums;
using Wonga.QA.Framework.Msmq.Enums.Common.Enums;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.Commands
{
    /// <summary> Wonga.Comms.Commands.SaveCustomerDetailsMessage </summary>
    [XmlRoot("SaveCustomerDetailsMessage", Namespace = "Wonga.Comms.Commands", DataType = "" )
    , SourceAssembly("Wonga.Comms.Commands, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class SaveCustomerDetails : MsmqMessage<SaveCustomerDetails>
    {
        public Guid AccountId { get; set; }
        public DateTime DateOfBirth { get; set; }
        public TitleEnum? Title { get; set; }
        public GenderEnum Gender { get; set; }
        public String Forename { get; set; }
        public String Surname { get; set; }
        public String MiddleName { get; set; }
        public String HomePhone { get; set; }
        public String WorkPhone { get; set; }
        public String MobilePhone { get; set; }
        public String Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}
