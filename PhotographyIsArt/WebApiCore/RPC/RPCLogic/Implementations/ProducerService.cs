using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.RPC.RPCLogic.Interfaces;

namespace WebApiCore.RPC.RPCLogic.Implementations
{
	internal class ProducerService : IProducerService
	{
		private readonly IConnectionFactory _connectionFactory;

		public ProducerService(IConnectionFactory connectionFactory)
		{
			_connectionFactory = connectionFactory;
		}

		public async Task<string> CallAsync(string message, CancellationToken cancellationToken = default)
		{
			var client = new ProducerClient(_connectionFactory);
			var response = await client.CallAsync(message, cancellationToken);
			return response;
		}
	}
}
