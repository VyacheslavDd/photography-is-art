using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiCore.Exceptions;
using WebApiCore.Exceptions.Enums;
using WebApiCore.Http.HttpEnums;
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
			var response = await _httpRequestService.SendRequestAsync<CheckUserExistenceResponse>(request, connectionData);
			if (!response.IsSuccessStatusCode) ExceptionHandler.ThrowException(ExceptionType.UserDoesNotExist, "Пользователя с указанным Guid не существует!");
		}
	}
}
