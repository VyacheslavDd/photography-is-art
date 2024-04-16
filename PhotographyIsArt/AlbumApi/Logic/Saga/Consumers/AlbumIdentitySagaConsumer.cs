using AlbumApi.Logic.Saga.Models;
using MassTransit;
using WebApiCore.Libs.AlbumUserConnectionService.Interfaces;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Requests;

namespace AlbumApi.Logic.Saga.Consumers
{
	public class AlbumIdentitySagaConsumer : IConsumer<CreateAlbumSagaRequest>
	{
		private readonly IAlbumUserConnectionService _albumUserConnectionService;

		public AlbumIdentitySagaConsumer(IAlbumUserConnectionService albumUserConnectionService)
		{
			_albumUserConnectionService = albumUserConnectionService;
		}

		public async Task Consume(ConsumeContext<CreateAlbumSagaRequest> context)
		{
			try
			{
				await _albumUserConnectionService.CheckUserExistenseAsync(new CheckUserExistenceRequest() { UserId = context.Message.UserId });
				await context.Publish<CreateAlbumSagaResponse>(new
				{
					context.Message.AlbumId,
					context.Message.UserId
				});
			}
			catch (Exception ex)
			{
				await context.Publish<CreatingAlbumFailed>(new
				{
					ex.Message,
					context.Message.AlbumId,
					context.Message.UserId
				});
			}
		}
	}
}
