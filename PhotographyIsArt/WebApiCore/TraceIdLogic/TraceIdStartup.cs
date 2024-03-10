using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.TraceIdLogic.Interfaces;
using WebApiCore.TraceLogic.Interfaces;

namespace WebApiCore.TraceIdLogic
{
	public static class TraceIdStartup
	{
		public static IServiceCollection TryAddTraceId(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddScoped<TraceIdAccessor>();
			serviceCollection
				.TryAddScoped<ITraceWriter>(provider => provider.GetRequiredService<TraceIdAccessor>());
			serviceCollection
				.TryAddScoped<ITraceReader>(provider => provider.GetRequiredService<TraceIdAccessor>());
			serviceCollection
				.TryAddScoped<ITraceIdAccessor>(provider => provider.GetRequiredService<TraceIdAccessor>());

			return serviceCollection;
		}
	}
}
