using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Http.HttpLogic.Services.Interfaces;
using WebApiCore.Http.HttpModels.Data;

namespace WebApiCore.Http.HttpLogic.Services
{
    internal class HttpConnectionService : IHttpConnectionService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpConnectionService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient CreateHttpClient(HttpConnectionData httpConnectionData)
        {
            var httpClient = string.IsNullOrWhiteSpace(httpConnectionData.ClientName)
                ? _httpClientFactory.CreateClient()
                : _httpClientFactory.CreateClient(httpConnectionData.ClientName);

            if (httpConnectionData.Timeout != null)
            {
                httpClient.Timeout = httpConnectionData.Timeout.Value;
            }

            return httpClient;
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage httpRequestMessage, HttpClient httpClient, CancellationToken cancellationToken, HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead)
        {
            var response = await httpClient.SendAsync(httpRequestMessage, httpCompletionOption, cancellationToken);
            return response;
        }
    }
}
