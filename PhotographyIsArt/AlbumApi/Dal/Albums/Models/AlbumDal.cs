using AlbumApi.Dal.Tags.Models;
using WebApiCore.Dal.Base.Models;

namespace AlbumApi.Dal.Albums.Models
{
	public record AlbumDal : BaseModel<Guid>
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? CreationDate { get; set; }
		public string? AlbumCoverImageUrl { get; set; }
		public string? AlbumPicturesUrls { get; set; }
		public string? LastRedactedDate { get; set; }
		public required Guid UserId { get; set; }
		public List<AlbumTagDal> Tags { get; set; }
	}
}
