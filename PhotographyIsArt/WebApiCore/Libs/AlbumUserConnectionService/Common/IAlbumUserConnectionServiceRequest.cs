using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Requests;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Responses;

namespace WebApiCore.Libs.AlbumUserConnectionService.Common
{
	public interface IAlbumUserConnectionServiceRequest
	{
		Task CheckUserExistenceAsync(CheckUserExistenceRequest request);
		Task<GetShortUserInfoResponse> GetShortUserInfoAsync(GetShortUserInfoRequest request);
		Task<List<GetUserAlbumsResponse>> GetUserAlbumsAsync(GetUserAlbumsRequest request);
	}
}
