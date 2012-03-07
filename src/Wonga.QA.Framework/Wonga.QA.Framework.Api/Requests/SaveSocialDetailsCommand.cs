using System;
using System.Xml.Serialization;

namespace Wonga.QA.Framework.Api
{
    /// <summary> Wonga.Risk.Commands.SaveSocialDetails </summary>
    [XmlRoot("SaveSocialDetails")]
    public partial class SaveSocialDetailsCommand : ApiRequest<SaveSocialDetailsCommand>
    {
        public Object AccountId { get; set; }
        public Object MaritalStatus { get; set; }
        public Object OccupancyStatus { get; set; }
        public Object Dependants { get; set; }
        public Object VehicleRegistration { get; set; }
    }
}
