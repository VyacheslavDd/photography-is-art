using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.RPC.RPCLogic.Interfaces
{
	public interface IProducerClient
	{
		Task<string> CallAsync(string message, CancellationToken cancellationToken = default);
		void OnReceived(object model, BasicDeliverEventArgs eventArgs);
	}
}
