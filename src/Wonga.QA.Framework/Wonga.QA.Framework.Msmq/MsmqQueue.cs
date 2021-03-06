﻿using System;
using System.Diagnostics;
using System.Messaging;
using System.Security.Principal;
using System.Text;
using Wonga.QA.Framework.Core;

namespace Wonga.QA.Framework.Msmq
{
    public class MsmqQueue
    {
        private MessageQueue _queue;
        private MessageQueue _journal;
        private MessageQueue _error;
        private MessageQueue _subscriptions;

        private MessageQueue _response;
        private MessageQueue _administration;

        public MsmqQueue(String path)
        {
            _queue = new MessageQueue(path);
            _journal = new MessageQueue(String.Format("{0};journal", _queue.Path));
            _error = new MessageQueue(String.Format("{0}_error", _queue.Path));
            _subscriptions = new MessageQueue(String.Format("{0}_subscriptions", _queue.Path));

            _response = new MessageQueue(String.Format("{0}\\qa.response", _queue.Path.Substring(0, _queue.Path.LastIndexOf('\\'))));
            _administration = new MessageQueue(String.Format("{0}\\qa.administration", _queue.Path.Substring(0, _queue.Path.LastIndexOf('\\'))));
        }

        static MsmqQueue()
        {
            if (Config.SUT == SUT.Dev)
            {
                String response = @".\private$\qa.response";
                String administration = @".\private$\qa.administration";
                String user = new SecurityIdentifier(WellKnownSidType.WorldSid, null).Translate(typeof(NTAccount)).ToString();

                if (!MessageQueue.Exists(response))
                    MessageQueue.Create(response, true).SetPermissions(user, MessageQueueAccessRights.GenericRead | MessageQueueAccessRights.GenericWrite);

                if (!MessageQueue.Exists(administration))
                    MessageQueue.Create(administration).SetPermissions(user, MessageQueueAccessRights.GenericRead | MessageQueueAccessRights.GenericWrite);
            }
        }

        public void Send(MsmqMessage message,string label = "")
        {
            Send(message.ToString(),label);
        }

		public void Send(string body,string label)
		{
			SendMessageToQueue(body, label,_queue);
		}

		public void SendSubscription(string body, string label)
		{
			SendMessageToQueue(body, label, _subscriptions);
		}

    	private void SendMessageToQueue(string body, string label,MessageQueue destinationQueue)
    	{
    		Trace.WriteLine(Get.Indent(body), GetType().FullName);

    		Byte[] bytes = Encoding.Default.GetBytes(body);
    		Message send = new Message
    		               	{
    		               		ResponseQueue = _response,
    		               		AdministrationQueue = _administration,
    		               		AcknowledgeType = AcknowledgeTypes.FullReceive,
    		               		Label = label
    		               	};
    		send.BodyStream.Write(bytes, 0, bytes.Length);
    		destinationQueue.Send(send, MessageQueueTransactionType.Single);
    	}

    	private MsmqQueue Wait(MsmqMessage message)
        {
            throw new NotImplementedException();
        }

        private MsmqMessage Find(Guid id)
        {
            throw new NotImplementedException();
        }

        private MsmqQueue Purge()
        {
            throw new NotImplementedException();
        }
    }
}
