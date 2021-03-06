using Wonga.QA.Framework.Core;
using Wonga.QA.Framework.Msmq;

namespace Wonga.QA.ServiceTests.Risk
{
	public interface IMessageFactory
	{
		MessageBase MessageBase { get; }
		void Initialise();
		void Instantiate();
		void ApplyDefaults();
	}
}