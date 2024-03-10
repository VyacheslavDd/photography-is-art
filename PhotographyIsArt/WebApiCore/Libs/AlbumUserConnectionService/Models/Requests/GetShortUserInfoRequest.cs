using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Libs.AlbumUserConnectionService.Models.Requests
{
	public class GetShortUserInfoRequest
	{
		public required Guid UserId { get; set; }
	}
}
