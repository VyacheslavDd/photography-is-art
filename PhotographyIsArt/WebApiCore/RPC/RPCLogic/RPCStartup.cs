using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.RPC.RPCLogic.Implementations;
using WebApiCore.RPC.RPCLogic.Interfaces;

namespace WebApiCore.RPC.RPCLogic
{
	public static class RPCStartup
	{
		public static IServiceCollection TryAddRPC(this IServiceCollection services)
		{
			services.TryAddSingleton<IConnectionFactory, ConnectionFactory>();
			services.AddHostedService<ConsumerService>();
			services.TryAddScoped<IProducerService, ProducerService>();
			return services;
		}
	}
}
