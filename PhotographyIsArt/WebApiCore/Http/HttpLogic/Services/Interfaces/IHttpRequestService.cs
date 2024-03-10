using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Http.HttpLogic.Services;
using WebApiCore.Http.HttpModels.Data;
using WebApiCore.Http.HttpModels.Response;

namespace WebApiCore.Http.HttpLogic.Services.Interfaces
{
    public interface IHttpRequestService
    {
        Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData, HttpConnectionData connectionData = default);
    }
}
