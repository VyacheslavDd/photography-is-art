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

namespace WebApiCore.Http.HttpLogic.Services
{

	/// <inheritdoc />
	internal class HttpRequestService : IHttpRequestService
	{
		private readonly IHttpConnectionService _httpConnectionService;
		private readonly IEnumerable<ITraceWriter> _traceWriterList;

		///
		public HttpRequestService(
			IHttpConnectionService httpConnectionService,
			IEnumerable<ITraceWriter> traceWriterList)
		{
			_httpConnectionService = httpConnectionService;
			_traceWriterList = traceWriterList;
		}

		/// <inheritdoc />
		public async Task<HttpResponse<TResponse>> SendRequestAsync<TResponse>(HttpRequestData requestData,
			HttpConnectionData connectionData)
		{
			var client = _httpConnectionService.CreateHttpClient(connectionData);

			var httpRequestMessage = new HttpRequestMessage();

			foreach (var traceWriter in _traceWriterList)
			{
				httpRequestMessage.Headers.Add(traceWriter.Name, traceWriter.GetValue());
			}

			var res = await _httpConnectionService.SendRequestAsync(httpRequestMessage, null, default);
			return null;
		}

		private static HttpContent PrepairContent(object body, ContentType contentType)
		{
			switch (contentType)
			{
				case ContentType.ApplicationJson:
					{
						if (body is string stringBody)
						{
							body = JToken.Parse(stringBody);
						}

						var serializeSettings = new JsonSerializerSettings
						{
							ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
							NullValueHandling = NullValueHandling.Ignore
						};
						var serializedBody = JsonConvert.SerializeObject(body, serializeSettings);
						var content = new StringContent(serializedBody, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Json);
						return content;
					}

				case ContentType.XWwwFormUrlEncoded:
					{
						if (body is not IEnumerable<KeyValuePair<string, string>> list)
						{
							throw new Exception(
								$"Body for content type {contentType} must be {typeof(IEnumerable<KeyValuePair<string, string>>).Name}");
						}

						return new FormUrlEncodedContent(list);
					}
				case ContentType.ApplicationXml:
					{
						if (body is not string s)
						{
							throw new Exception($"Body for content type {contentType} must be XML string");
						}

						return new StringContent(s, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Application.Xml);
					}
				case ContentType.Binary:
					{
						if (body.GetType() != typeof(byte[]))
						{
							throw new Exception($"Body for content type {contentType} must be {typeof(byte[]).Name}");
						}

						return new ByteArrayContent((byte[])body);
					}
				case ContentType.TextXml:
					{
						if (body is not string s)
						{
							throw new Exception($"Body for content type {contentType} must be XML string");
						}

						return new StringContent(s, Encoding.UTF8, System.Net.Mime.MediaTypeNames.Text.Xml);
					}
				default:
					throw new ArgumentOutOfRangeException(nameof(contentType), contentType, null);
			}
		}
	}
}
