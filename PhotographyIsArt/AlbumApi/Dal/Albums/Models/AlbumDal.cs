using AlbumApi.Dal.Tags.Models;
using WebApiCore.Dal.Base.Models;

namespace AlbumApi.Dal.Albums.Models
{
	//буду добавлять сюда User Guid, когда создам сам сервис связанный с пользователями (его в качестве второго дз на Onion архитектуре), а пока без этого
	public record AlbumDal : BaseModel<Guid>
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? CreationDate { get; set; }
		public string? AlbumCoverImageUrl { get; set; }
		public string? AlbumPicturesUrls { get; set; }
		public string? LastRedactedDate { get; set; }
		public List<AlbumTagDal> Tags { get; set; }
	}
}
