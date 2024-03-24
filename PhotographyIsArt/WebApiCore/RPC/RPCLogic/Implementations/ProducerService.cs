using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.RPC.RPCLogic.Interfaces;
using WebApiCore.RPC.RPCModels.Data;

namespace WebApiCore.RPC.RPCLogic.Implementations
{
	internal class ProducerService : IProducerService
	{
		private readonly IConnectionFactory _connectionFactory;
		private readonly IOptions<RPCConfigurationData> _rpcConfig;

		public ProducerService(IConnectionFactory connectionFactory, IOptions<RPCConfigurationData> rpcConfig)
		{
			_connectionFactory = connectionFactory;
			_rpcConfig = rpcConfig;
		}

		public async Task<string> CallAsync(string message, CancellationToken cancellationToken = default)
		{
			var client = new ProducerClient(_connectionFactory, _rpcConfig);
			var response = await client.CallAsync(message, cancellationToken);
			return response;
		}
	}
}
