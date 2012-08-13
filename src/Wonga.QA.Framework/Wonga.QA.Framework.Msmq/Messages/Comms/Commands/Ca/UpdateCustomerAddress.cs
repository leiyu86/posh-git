using System;
using System.Collections.Generic;
using System.Xml.Serialization;

using Wonga.QA.Framework.Msmq.Enums.Integration.Comms.Enums;

namespace Wonga.QA.Framework.Msmq.Messages.Comms.Commands.Ca
{
    /// <summary> Wonga.Comms.Commands.Ca.UpdateCustomerAddressCaMessage </summary>
    [XmlRoot("UpdateCustomerAddressCaMessage", Namespace = "Wonga.Comms.Commands.Ca", DataType = "" )
    , SourceAssembly("Wonga.Comms.Commands.Ca, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null")]
    public partial class UpdateCustomerAddress : MsmqMessage<UpdateCustomerAddress>
    {
        public String PoBox { get; set; }
        public Guid AddressId { get; set; }
        public Guid AccountId { get; set; }
        public String Flat { get; set; }
        public String HouseNumber { get; set; }
        public String HouseName { get; set; }
        public String Street { get; set; }
        public String District { get; set; }
        public String Town { get; set; }
        public String County { get; set; }
        public String Postcode { get; set; }
        public CountryCodeEnum CountryCode { get; set; }
        public DateTime AtAddressFrom { get; set; }
        public DateTime? AtAddressTo { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ClientId { get; set; }
    }
}
