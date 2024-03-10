using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using WebApiCore.Http.HttpLogic.Services;
using WebApiCore.Http.HttpLogic.Services.Interfaces;

namespace WebApiCore.Http.HttpLogic
{
    public static class HttpServiceStartup
    {
        public static IServiceCollection AddHttpRequestService(this IServiceCollection services)
        {
            services
                .AddHttpContextAccessor()
                .AddHttpClient()
                .AddTransient<IHttpConnectionService, HttpConnectionService>();

            services.TryAddTransient<IHttpRequestService, HttpRequestService>();

            return services;
        }
    }
}
