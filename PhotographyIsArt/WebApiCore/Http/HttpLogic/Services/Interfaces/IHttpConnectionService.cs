using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Http.HttpLogic.Services;
using WebApiCore.Http.HttpModels.Data;

namespace WebApiCore.Http.HttpLogic.Services.Interfaces
{
    public interface IHttpConnectionService
    {
        HttpClient CreateHttpClient(HttpConnectionData httpConnectionData);

        Task<HttpResponseMessage> SendRequestAsync(
            HttpRequestMessage httpRequestMessage,
            HttpClient httpClient,
            CancellationToken cancellationToken,
            HttpCompletionOption httpCompletionOption = HttpCompletionOption.ResponseContentRead);
    }
}
