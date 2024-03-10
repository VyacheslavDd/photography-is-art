using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Requests;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Responses;

namespace WebApiCore.Libs.AlbumUserConnectionService.Interfaces
{
	public interface IAlbumUserConnectionService
	{
		Task CheckUserExistenseAsync(CheckUserExistenceRequest userRequest);
	}
}
