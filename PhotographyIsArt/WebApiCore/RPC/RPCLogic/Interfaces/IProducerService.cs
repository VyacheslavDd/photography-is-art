using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Http.HttpModels.Data;
using WebApiCore.Http.HttpModels.Response;
using WebApiCore.RPC.RPCModels.Data;

namespace WebApiCore.RPC.RPCLogic.Interfaces
{
	public interface IProducerService
	{
		Task<TResponse> CallAsync<TRequest, TResponse>(TRequest request);
	}
}
