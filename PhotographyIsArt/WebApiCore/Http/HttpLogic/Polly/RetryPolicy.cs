using Polly;
using Polly.Extensions.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Http.HttpLogic.Polly
{
	public class RetryPolicy
	{
		public static async Task<HttpResponseMessage> RetryAsync(Func<Task<HttpResponseMessage>> action, int retryCount)
		{
			return await Policy
							.Handle<HttpRequestException>().OrResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
							.WaitAndRetryAsync(retryCount, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)))
							.ExecuteAsync(action);
		}
	}
}
