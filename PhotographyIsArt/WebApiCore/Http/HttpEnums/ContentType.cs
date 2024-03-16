using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Http.HttpEnums
{
	public enum ContentType
	{
		Unknown = 0,
		ApplicationJson = 1,
		XWwwFormUrlEncoded = 2,
		Binary = 3,
		ApplicationXml = 4,
		MultipartFormData = 5,
		TextXml = 6,
		TextPlain = 7,
		ApplicationJwt = 8
	}
}
