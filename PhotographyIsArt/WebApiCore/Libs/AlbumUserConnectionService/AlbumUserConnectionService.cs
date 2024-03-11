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
using WebApiCore.Libs.AlbumUserConnectionService.Interfaces;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Requests;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Responses;

namespace WebApiCore.Libs.AlbumUserConnectionService
{
	internal class AlbumUserConnectionService : IAlbumUserConnectionService
	{
		private readonly IHttpRequestService _httpRequestService;

		//здесь буду потом расширять до возможности переключиться на RPC по rabbit
		public AlbumUserConnectionService(IServiceProvider serviceProvider)
		{
			_httpRequestService = serviceProvider.GetRequiredService<IHttpRequestService>();
		}

		public async Task CheckUserExistenseAsync(CheckUserExistenceRequest userRequest)
		{
			var connectionData = new HttpConnectionData()
			{
				ClientName = "existence check",
				Timeout = new TimeSpan(0, 0, 10),
				CancellationToken = new CancellationTokenSource().Token
			};
			var request = new HttpRequestData()
			{
				Method = HttpMethod.Get,
				Uri = (new UriBuilder("https://localhost:7225") { Path = $"api/users/{userRequest.UserId}/short" }).Uri,
				Body = null
			};
			var retryData = new RetryData() { RetryCount = 3 };
			var response = await _httpRequestService.SendRequestAsync<CheckUserExistenceResponse>(request, connectionData, retryData);
			if (!response.IsSuccessStatusCode) ExceptionHandler.ThrowException(ExceptionType.UserDoesNotExist, "Пользователя с указанным Guid не существует!");
		}

		public async Task<GetShortUserInfoResponse> GetShortUserInfoAsync(GetShortUserInfoRequest userRequest)
		{
			var connectionData = new HttpConnectionData()
			{
				ClientName = "short user info",
				Timeout = new TimeSpan(0, 0, 10),
				CancellationToken = new CancellationTokenSource().Token
			};
			var request = new HttpRequestData()
			{
				Method = HttpMethod.Get,
				Uri = (new UriBuilder("https://localhost:7225") { Path = $"api/users/{userRequest.UserId}/short" }).Uri,
				Body = null
			};
			var retryData = new RetryData() { RetryCount = 3 };
			var response = await _httpRequestService.SendRequestAsync<GetShortUserInfoResponse>(request, connectionData, retryData);
			if (!response.IsSuccessStatusCode) ExceptionHandler.ThrowException(ExceptionType.UserDoesNotExist, "Пользователя с указанным Guid не существует!");
			return response.Body;
		}

		public async Task<List<GetUserAlbumsResponse>> GetUserAlbumsAsync(GetUserAlbumsRequest userRequest)
		{
			var connectionData = new HttpConnectionData()
			{
				ClientName = "user albums",
				Timeout = new TimeSpan(0, 0, 10),
				CancellationToken = new CancellationTokenSource().Token
			};
			var request = new HttpRequestData()
			{
				Method = HttpMethod.Get,
				Uri = (new UriBuilder("https://localhost:7204") { Path = $"api/albums/all" }).Uri,
				Body = null,
				QueryParameterList = new Dictionary<string, string>() 
				{ 
					{ "userId", userRequest.UserId.ToString() },
					{ "collectUserData", userRequest.CollectUserData.ToString() }
				}
			};
			var retryData = new RetryData() { RetryCount = 3 };
			var response = await _httpRequestService.SendRequestAsync<List<GetUserAlbumsResponse>>(request, connectionData, retryData);
			if (!response.IsSuccessStatusCode) ExceptionHandler.ThrowException(ExceptionType.IncorrentArgument, "Передан некорректный Guid!");
			return response.Body;
		}
	}
}
