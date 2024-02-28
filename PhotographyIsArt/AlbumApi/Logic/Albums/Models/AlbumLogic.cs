using AlbumApi.Logic.Tags.Models;

namespace AlbumApi.Logic.Albums.Models
{
	public class AlbumLogic
	{
		public Guid Guid { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? CreationDate { get; set; }
		public string? LastRedactedDate { get; set; }
		public string? AlbumCoverImageUrl { get; set; }
		public string? AlbumPicturesUrls { get; set; }
		public List<AlbumTagLogic> Tags { get; set; }
	}
}
