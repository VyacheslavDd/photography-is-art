using AlbumApi.Api.Controllers.Tags.ResponseModels;

namespace AlbumApi.Api.Controllers.Albums.ResponseModels
{
	public class GetAlbumResponse
	{
		public Guid Guid { get; set; }
		public required string UserName { get; set; }
		public required string UserProfilePictureUrl { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? CreationDate { get; set; }
		public string? LastRedactedDate { get; set; }
		public string? AlbumCoverImageUrl { get; set; }
		public string? AlbumPicturesUrls { get; set; }
		public List<GetTagResponse> Tags { get; set; }
	}
}
