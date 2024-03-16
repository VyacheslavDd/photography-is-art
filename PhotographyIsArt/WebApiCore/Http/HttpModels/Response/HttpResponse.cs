using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Http.HttpModels.Response
{
	public record HttpResponse<TResponse> : BaseHttpResponse
	{
		public TResponse Body { get; set; }
	}
}
