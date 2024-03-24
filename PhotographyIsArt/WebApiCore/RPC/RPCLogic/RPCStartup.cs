using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Pool.Logic;
using WebApiCore.Pool.Logic.Interfaces;
using WebApiCore.RPC.RPCLogic.Implementations;
using WebApiCore.RPC.RPCLogic.Interfaces;
using WebApiCore.RPC.RPCModels.Data;

namespace WebApiCore.RPC.RPCLogic
{
	public static class RPCStartup
	{
		public static IServiceCollection TryAddRPC(this IServiceCollection services, IConfiguration conf, string section)
		{
			var rpcConfig = conf.GetSection(section);
			services.Configure<RPCConfigurationData>(opt => rpcConfig.Bind(opt));
			services.TryAddSingleton<IConnectionFactory, ConnectionFactory>();
			services.AddHostedService<ConsumerService>();
			services.TryAddScoped<IProducerService, ProducerService>();
			services.TryAddSingleton<IPool<IConnection>, RPCConnectionPool>();
			return services;
		}
	}
}
