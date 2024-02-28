using AlbumApi.Dal.Albums.Models;
using AlbumApi.Logic.Albums.Models;

namespace AlbumApi.Logic.Tags.Models
{
	public class AlbumTagLogic
	{
		public Guid Guid { get; set; }
		public string? Name { get; set; }
		public List<AlbumLogic> Albums { get; set; }
	}
}
