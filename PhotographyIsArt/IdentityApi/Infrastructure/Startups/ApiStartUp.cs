using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;

namespace IdentityApi.Infrastructure.Startups
{
	public static class ApiStartUp
	{
		public static IServiceCollection TryAddApi(this IServiceCollection services)
		{
			services.AddControllers().AddNewtonsoftJson(opt =>
			{
				opt.SerializerSettings.Converters.Add(new StringEnumConverter
				{
					NamingStrategy = new CamelCaseNamingStrategy()
				});
			})
				.AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
			return services;
		}
	}
}
