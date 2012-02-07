using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Msmq.Comms
{
    [XmlRoot("SaveCustomerDetailsMessage", Namespace = "Wonga.Comms.Commands.Za", DataType = "")]
    public partial class SaveCustomerDetailsZaCommand : MsmqMessage<SaveCustomerDetailsZaCommand>
    {
        public String NationalNumber { get; set; }
        public Boolean MarriedInCommunityProperty { get; set; }
        public LanguageEnum? HomeLanguage { get; set; }
        public String MaidenName { get; set; }
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
