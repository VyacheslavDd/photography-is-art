using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Libs.AlbumUserConnectionService.Models.Responses
{
	public class GetShortUserInfoResponse
	{
		public required string Name { get; set; }
		public required string ProfilePictureUrl { get; set; }
	}
}
