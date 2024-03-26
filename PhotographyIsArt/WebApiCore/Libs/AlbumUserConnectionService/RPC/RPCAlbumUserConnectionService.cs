using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Requests;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Responses;
using WebApiCore.RPC.RPCLogic.Interfaces;

namespace WebApiCore.Libs.AlbumUserConnectionService.RPC
{
	internal class RPCAlbumUserConnectionService : IRPCAlbumUserConnectionService
	{
		private readonly IProducerService _producerService;

		public RPCAlbumUserConnectionService(IProducerService producerService)
		{
			_producerService = producerService;
		}

		public async Task CheckUserExistenceAsync(CheckUserExistenceRequest request)
		{
			await _producerService.CallAsync<CheckUserExistenceRequest, CheckUserExistenceResponse>(request);
		}

		public async Task<GetShortUserInfoResponse> GetShortUserInfoAsync(GetShortUserInfoRequest request)
		{
			return await _producerService.CallAsync<GetShortUserInfoRequest, GetShortUserInfoResponse>(request);
		}

		public async Task<List<GetUserAlbumsResponse>> GetUserAlbumsAsync(GetUserAlbumsRequest request)
		{
			return await _producerService.CallAsync<GetUserAlbumsRequest, List<GetUserAlbumsResponse>>(request);
		}
	}
}
