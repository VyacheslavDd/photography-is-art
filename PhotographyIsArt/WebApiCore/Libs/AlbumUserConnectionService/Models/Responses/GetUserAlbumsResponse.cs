using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiCore.Libs.AlbumUserConnectionService.Models.Responses
{
	public class GetUserAlbumsResponse
	{
		public required string Title { get; set; }
		public required string Description { get; set; }
		public required string AlbumCoverImageUrl { get; set; }
	}
}
