using IdentityApi.Infrastructure.Filters;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IdentityApi.Infrastructure.Startups
{
	public static class FiltersStartUp
	{
		public static IServiceCollection TryAddFilters(this IServiceCollection services)
		{
			services.TryAddScoped<GetUserResultFilter>();
			return services;
		}
	}
}
