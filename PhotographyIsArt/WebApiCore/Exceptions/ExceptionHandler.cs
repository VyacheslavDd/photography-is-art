using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Exceptions.Enums;

namespace WebApiCore.Exceptions
{
	public static class ExceptionHandler
	{
		public static void ThrowException(ExceptionType exc, string message)
		{
			throw new Exception($"{exc.GetType().GetEnumName(exc)}: {message}");
		}
	}
}
