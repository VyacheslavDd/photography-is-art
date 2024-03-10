using Microsoft.AspNetCore.Mvc.Filters;

namespace IdentityApi.Infrastructure.Filters
{
	public class GetUserResultFilter : IResultFilter
	{
		public void OnResultExecuted(ResultExecutedContext context)
		{
			if (context.HttpContext.Response.StatusCode == 204)
			{
				context.HttpContext.Response.StatusCode = 404;
			}
		}

		public void OnResultExecuting(ResultExecutingContext context)
		{
		}
	}
}
