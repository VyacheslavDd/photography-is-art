using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.RPC.RPCModels.Data
{
	public class RPCConfigurationData
	{
		public required string ExchangeName { get; set; }
		public required string ExchangeType { get; set; }
		public required string RoutingKey { get; set; }
		public required string ConsumerQueue {  get; set; }
		public required bool IsDurable { get; set; }
		public required bool IsExclusive { get; set; }
		public required bool AutoDelete { get; set; }
		public required uint PrefetchSize { get; set; }
		public required ushort PrefetchCount { get; set; }
		public required bool IsGlobal { get; set; }
	}
}
