using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.RPC.RPCLogic.Interfaces
{
	public interface IProducerService
	{
		Task<string> CallAsync(string message, CancellationToken cancellationToken = default);
	}
}
