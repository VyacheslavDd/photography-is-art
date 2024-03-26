using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Exceptions;
using WebApiCore.Exceptions.Enums;
using WebApiCore.Http.HttpEnums;
using WebApiCore.Http.HttpLogic.Polly;
using WebApiCore.Http.HttpLogic.Services.Interfaces;
using WebApiCore.Http.HttpModels.Data;
using WebApiCore.Libs.AlbumUserConnectionService.Common;
using WebApiCore.Libs.AlbumUserConnectionService.Http;
using WebApiCore.Libs.AlbumUserConnectionService.Interfaces;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Requests;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Responses;
using WebApiCore.Libs.AlbumUserConnectionService.RPC;

namespace WebApiCore.Libs.AlbumUserConnectionService
{
	internal class AlbumUserConnectionService : IAlbumUserConnectionService
	{
		private readonly IAlbumUserConnectionServiceRequest _albumUserConnectionServiceRequest;

		public AlbumUserConnectionService(IServiceProvider serviceProvider, IConfiguration config)
		{
			var connectionSection = config.GetSection("ConnectionType");
			if (connectionSection == null || connectionSection.Value != "RPC")
				_albumUserConnectionServiceRequest = serviceProvider.GetRequiredService<IHttpAlbumUserConnectionService>();
			else
				_albumUserConnectionServiceRequest = serviceProvider.GetRequiredService<IRPCAlbumUserConnectionService>();
		}

		public async Task CheckUserExistenseAsync(CheckUserExistenceRequest userRequest)
		{
			await _albumUserConnectionServiceRequest.CheckUserExistenceAsync(userRequest);
		}

		public async Task<GetShortUserInfoResponse> GetShortUserInfoAsync(GetShortUserInfoRequest userRequest)
		{
			return await _albumUserConnectionServiceRequest.GetShortUserInfoAsync(userRequest);
		}

		public async Task<List<GetUserAlbumsResponse>> GetUserAlbumsAsync(GetUserAlbumsRequest userRequest)
		{
			return await _albumUserConnectionServiceRequest.GetUserAlbumsAsync(userRequest);
		}
	}
}
