using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api.Requests.Comms.Commands.Za
{
    /// <summary> Wonga.Comms.Commands.Za.SaveCustomerDetailsZa </summary>
    [XmlRoot("SaveCustomerDetailsZa")]
    public partial class SaveCustomerDetailsZaCommand : ApiRequest<SaveCustomerDetailsZaCommand>
    {
        public Object AccountId { get; set; }
        public Object DateOfBirth { get; set; }
        public Object Title { get; set; }
        public Object Gender { get; set; }
        public Object Forename { get; set; }
        public Object Surname { get; set; }
        public Object MiddleName { get; set; }
        public Object HomePhone { get; set; }
        public Object WorkPhone { get; set; }
        public Object Email { get; set; }
        public Object NationalNumber { get; set; }
        public Object MarriedInCommunityProperty { get; set; }
        public Object HomeLanguage { get; set; }
        public Object MaidenName { get; set; }
    }
}
