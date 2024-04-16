namespace AlbumApi.Logic.Saga.Models
{
	public class CreateAlbumSagaRequest
	{
		public required Guid AlbumId { get; set; }
		public required Guid UserId { get; set; }
	}
}
