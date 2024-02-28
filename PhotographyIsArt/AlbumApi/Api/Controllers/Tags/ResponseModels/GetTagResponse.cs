using AlbumApi.Api.Controllers.Albums.ResponseModels;

namespace AlbumApi.Api.Controllers.Tags.ResponseModels
{
	public class GetTagResponse
	{
		public Guid Guid { get; set; }
		public string? Name { get; set; }
		public List<GetAlbumResponse> Albums { get; set; }
	}
}
