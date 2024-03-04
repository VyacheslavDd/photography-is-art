using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using System.Text.Json.Serialization;
using WebApiCore.Dal.Constants;
using WebApiCore.Logic.Base.Interfaces;

namespace IdentityApi.Infrastructure.Startups
{
	public static class ApiStartUp
	{
		private static IServiceCollection AddControllers(IServiceCollection services)
		{
			services.AddControllers().AddNewtonsoftJson(opt =>
			{
				opt.SerializerSettings.Converters.Add(new StringEnumConverter
				{
					NamingStrategy = new CamelCaseNamingStrategy()
				});
			}).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
			return services;
		}

		private static IServiceCollection AddSwagger(IServiceCollection services, string xmlPath)
		{
			services.AddSwaggerGen(setup =>
			{
				setup.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
				setup.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
				{
					Description = "User Authorizantion with JWT Bearer. Use (\"bearer {token}\"",
					In = ParameterLocation.Header,
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});
				setup.OperationFilter<SecurityRequirementsOperationFilter>();
			});
			return services;
		}

		private static IServiceCollection AddAuthentication(IServiceCollection services, IConfiguration config)
		{
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(opt =>
				{
					opt.TokenValidationParameters = new TokenValidationParameters()
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection(SpecialConstants.TokenSectionFullName).Value)),
						ValidateIssuer = false,
						ValidateAudience = false
					};
				});
			return services;
		}

		public static IServiceCollection TryAddApi(this IServiceCollection services, IConfiguration config, string xmlPath)
		{
			services = AddControllers(services);
			services = AddSwagger(services, xmlPath);
			services = AddAuthentication(services, config);
			return services;
		}
	}
}
