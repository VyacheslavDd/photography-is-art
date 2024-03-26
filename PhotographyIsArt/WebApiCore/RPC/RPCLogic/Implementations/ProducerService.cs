using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Http.HttpModels.Data;
using WebApiCore.Http.HttpModels.Response;
using WebApiCore.Pool.Logic;
using WebApiCore.Pool.Logic.Interfaces;
using WebApiCore.RPC.RPCLogic.Interfaces;
using WebApiCore.RPC.RPCModels.Data;

namespace WebApiCore.RPC.RPCLogic.Implementations
{
	internal class ProducerService : IProducerService
	{
		private readonly IOptions<RPCConfigurationData> _rpcConfig;
		private readonly IPool<IConnection> _connectionPool;

		public ProducerService(IOptions<RPCConfigurationData> rpcConfig, IPool<IConnection> pool)
		{
			_rpcConfig = rpcConfig;
			_connectionPool = pool;
		}

		public async Task<TResponse> CallAsync<TRequest, TResponse>(TRequest request)
		{
			var client = new ProducerClient<TResponse>(_rpcConfig, _connectionPool);
			using (var tokenSource = new CancellationTokenSource())
			{
				var response = await client.CallAsync(request, tokenSource.Token);
				return response;
			}
		}
	}
}
