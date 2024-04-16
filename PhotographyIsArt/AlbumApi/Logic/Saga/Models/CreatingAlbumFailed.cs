namespace AlbumApi.Logic.Saga.Models
{
	public class CreatingAlbumFailed
	{
		public required Guid AlbumId { get; set; }
		public required Guid UserId { get; set; }
		public required string Message { get; set; }
	}
}
