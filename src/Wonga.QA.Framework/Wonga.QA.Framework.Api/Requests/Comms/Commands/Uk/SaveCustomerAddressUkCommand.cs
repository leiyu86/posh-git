using System;
using System.Xml.Serialization;
using Wonga.QA.Framework.Api;

namespace Wonga.QA.Framework.Api.Requests.Comms.Commands.Uk
{
	[XmlRoot("SaveCustomerAddress")]
	public partial class SaveCustomerAddressUkCommand : ApiRequest<SaveCustomerAddressUkCommand>
	{
		public Object AddressId { get; set; }
		public Object AccountId { get; set; }
		public Object Flat { get; set; }
		public Object HouseNumber { get; set; }
		public Object HouseName { get; set; }
		public Object Street { get; set; }
		public Object District { get; set; }
		public Object Town { get; set; }
		public Object County { get; set; }
		public Object Postcode { get; set; }
		public Object CountryCode { get; set; }
		public Object AtAddressFrom { get; set; }
		public Object AtAddressTo { get; set; }
	}
}
