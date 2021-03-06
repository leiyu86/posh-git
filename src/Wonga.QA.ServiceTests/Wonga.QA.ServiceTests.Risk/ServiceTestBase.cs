using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using MbUnit.Framework;
using Wonga.QA.Framework;
using Wonga.QA.Framework.Api;
using Wonga.QA.Framework.Msmq;

namespace Wonga.QA.ServiceTests.Risk
{
	//TODO: Move to the Fx
	[TestFixture]
	public abstract class ServiceTestBase
	{
		protected string CardExpiryDateFormat = "yyyy-MM-dd";
		protected DateTime TestAsOf { get; set; }

		#region IDs
		protected Guid ApplicationId { get; private set; }

		protected virtual void GenerateIds()
		{
			ApplicationId = Guid.NewGuid();
			Console.WriteLine("Application ID: " + ApplicationId);
			Debug.WriteLine("Application ID: " + ApplicationId);
		}
		#endregion

		
		#region Message 

		protected virtual void DeclareCommands()
		{
			Messages = new MessageFactoryCollection();
		}

		protected void Send(IEnumerable<MsmqMessage> msgs)
		{
			msgs.ToList().ForEach(x => Drive.Msmq.Risk.Send(x));
		}

		protected void Send(MsmqMessage msg)
		{
			Send(new[]{msg});
		}

		protected void Post(IEnumerable<ApiRequest> requests)
		{
			requests.ToList().ForEach(x =>  Drive.Api.Commands.Post(x));
		}

		protected MessageFactoryCollection Messages { private set; get; }

		protected void SendAllMessages()
		{
			Send(Messages.MsmqMessages);
			Post(Messages.ApiRequests);
		}
		#endregion

		
		[SetUp]
		public void SetUp()
		{
			GenerateIds();
			DeclareCommands();

			Messages.Instantiate();
			Messages.ApplyDefaults();

			BeforeEachTest();
			Messages.Initialise();
		}
		
		[TearDown]
		public void TearDown()
		{
			AfterEachTest();
		}


		protected virtual void BeforeEachTest()
		{
			TestAsOf = DateTime.Now;
		}

		protected virtual void AfterEachTest(){}
	}
}