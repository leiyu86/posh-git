using System;
using System.Xml.Serialization;
using Wonga.QA.Framework.Api;

namespace Wonga.QA.Framework.Api.Requests.Risk.Commands.Uk
{
	[XmlRoot("SaveEmploymentDetails")]
	public partial class SaveEmploymentDetailsUkCommand : ApiRequest<SaveEmploymentDetailsUkCommand>
	{
		public Object AccountId { get; set; }
		public Object NetMonthlyIncome { get; set; }
		public Object IncomeFrequency { get; set; }
		public Object NextPayDate { get; set; }
		public Object Status { get; set; }
		public Object EmploymentIndustry { get; set; }
		public Object EmploymentPosition { get; set; }
		public Object EmployerName { get; set; }
		public Object StartDate { get; set; }
		public Object PaidDirectDeposit { get; set; }
	}
}
