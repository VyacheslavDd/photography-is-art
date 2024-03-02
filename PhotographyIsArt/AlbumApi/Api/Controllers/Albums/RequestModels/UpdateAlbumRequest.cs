namespace AlbumApi.Api.Controllers.Albums.RequestModels
{
	public class UpdateAlbumRequest
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public List<Guid> SelectedTags { get; set; }
		public IFormFile AlbumCoverImage { get; set; }
		public List<IFormFile> AlbumPictures { get; set; }
	}
}
