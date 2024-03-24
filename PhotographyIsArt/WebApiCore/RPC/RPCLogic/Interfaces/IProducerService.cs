using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.RPC.RPCModels.Data;

namespace WebApiCore.RPC.RPCLogic.Interfaces
{
	public interface IProducerService
	{
		Task<string> CallAsync(string message, CancellationToken cancellationToken = default);
	}
}
