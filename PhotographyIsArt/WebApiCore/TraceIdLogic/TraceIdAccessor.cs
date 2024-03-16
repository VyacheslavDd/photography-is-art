using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.TraceIdLogic.Interfaces;
using WebApiCore.TraceLogic.Interfaces;

namespace WebApiCore.TraceIdLogic
{
	internal class TraceIdAccessor : ITraceReader, ITraceWriter, ITraceIdAccessor
	{
		public string Name => "TraceId";

		private string _value;

		public string GetValue()
		{
			return _value;
		}

		public void WriteValue(string value)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				value = Guid.NewGuid().ToString();
			}

			_value = value;
			LogContext.PushProperty(Name, value);
		}
	}
}
