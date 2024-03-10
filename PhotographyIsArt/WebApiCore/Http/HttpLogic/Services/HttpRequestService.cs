using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Http.HttpLogic.Services.Interfaces;
using WebApiCore.TraceLogic.Interfaces;
using WebApiCore.Http.HttpModels.Data;
using WebApiCore.Http.HttpModels.Response;
using WebApiCore.Http.HttpEnums;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using WebApiCore.Http.HttpHelpers;
using System.Net.Http.Json;

namespace WebApiCore.Http.HttpLogic.Services
{

	/// <inheritdoc />
	internal class HttpRequestService : IHttpRequestService
	{
		private readonly IHttpConnectionService _httpConnectionService;
		private readonly IEnumerable<ITraceWriter> _traceWriterList;

		public HttpRequestService(
			IHttpConnectionService httpConnectionService,
			IEnumerable<ITraceWriter> traceWriterList)
		{
			_httpConnectionService = httpConnectionService;
			_traceWriterList = traceWriterList;
		}

		public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData,
			HttpConnectionData connectionData)
		{
			var client = _httpConnectionService.CreateHttpClient(connectionData);
			var requestMessage = HttpRequestServiceHelper.PrepairRequestMessage(requestData, _traceWriterList);
			using (var cancellationToken = new CancellationTokenSource())
			{
				var responseMessage = await _httpConnectionService.SendRequestAsync(requestMessage, client, cancellationToken.Token);
				var httpResponse = await HttpRequestServiceHelper.PrepairResponseMessageAsync<TResponse>(responseMessage);
				return httpResponse;
			}
		}

	}
}
