namespace AlbumApi.Logic.Saga.Models
{
	public class CreatingAlbumFinished
	{
		public required Guid AlbumId { get; set; }
		public required Guid UserId { get; set; }
	}
}
