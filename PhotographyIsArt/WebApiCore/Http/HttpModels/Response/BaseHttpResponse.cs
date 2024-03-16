using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Http.HttpModels.Response
{
	public record BaseHttpResponse
	{
		public HttpStatusCode StatusCode { get; set; }

		/// <summary>
		/// Заголовки, передаваемые в ответе
		/// </summary>
		public HttpResponseHeaders Headers { get; set; }

		/// <summary>
		/// Заголовки контента
		/// </summary>
		public HttpContentHeaders ContentHeaders { get; init; }

		public bool IsSuccessStatusCode
		{
			get
			{
				var statusCode = (int)StatusCode;

				return statusCode >= 200 && statusCode <= 299;
			}
		}
	}
}
