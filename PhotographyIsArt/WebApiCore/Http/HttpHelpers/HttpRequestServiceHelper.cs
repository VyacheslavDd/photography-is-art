using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Http.HttpModels.Data;
using WebApiCore.TraceLogic.Interfaces;
using WebApiCore.Http.HttpEnums;
using WebApiCore.Http.HttpModels.Response;
using System.Net.Http.Json;
using WebApiCore.Exceptions;
using WebApiCore.Exceptions.Enums;

namespace WebApiCore.Http.HttpHelpers
{
	public static class HttpRequestServiceHelper
	{
		public static HttpRequestMessage PrepairRequestMessage(HttpRequestData requestData, IEnumerable<ITraceWriter> traceList)
		{
			var message = new HttpRequestMessage
			{
				RequestUri = PrepairUriWithQueryParameters(requestData.Uri, requestData.QueryParameterList),
				Method = requestData.Method,
				Content = PrepairContent(requestData.Body, requestData.ContentType)
			};
			AddTracingParameters(message, traceList);
			AddHeaders(message, requestData.HeaderDictionary);
			return message;
		}

		public static async Task<HttpResponse<TResponse>> PrepairResponseMessageAsync<TResponse>(HttpResponseMessage responseMessage)
		{
			var httpResponse = new HttpResponse<TResponse>
			{
				StatusCode = responseMessage.StatusCode,
				Headers = responseMessage.Headers,
				ContentHeaders = responseMessage.Content.Headers
			};
			httpResponse.Body = httpResponse.IsSuccessStatusCode? await responseMessage.Content.ReadFromJsonAsync<TResponse>() : default(TResponse);
			return httpResponse;
		}

		private static void AddTracingParameters(HttpRequestMessage message, IEnumerable<ITraceWriter> traceList)
		{
			foreach (var traceWriter in traceList)
			{
				message.Headers.Add(traceWriter.Name, traceWriter.GetValue());
			}
		}

		private static void AddHeaders(HttpRequestMessage message, IDictionary<string, string> headers)
		{
            foreach (var header in headers)
            {
				message.Headers.Add(header.Key, header.Value);
            }
        }

		private static Uri PrepairUriWithQueryParameters(Uri uri, ICollection<KeyValuePair<string, string>> queryParameters)
		{
			var finalUri = new UriBuilder(uri);
			var queryString = new StringBuilder();
			foreach (var kvp in queryParameters)
			{
				queryString.Append($"{kvp.Key}={kvp.Value}&");
			}
			finalUri.Query = queryString.ToString();
			return finalUri.Uri;
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
