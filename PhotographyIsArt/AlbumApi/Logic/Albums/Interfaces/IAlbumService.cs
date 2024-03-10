using AlbumApi.Dal.Filters;
using AlbumApi.Logic.Albums.Models;

namespace AlbumApi.Logic.Albums.Interfaces
{
	public interface IAlbumService
	{
		Task<List<AlbumLogic>> GetAllAsync(AlbumQueryFilter filter);
		Task<AlbumLogic> GetByGuidAsync(Guid guid);
		Task<Guid> AddAsync(AlbumLogic album, List<Guid> tags, IFormFile albumCoverFile, List<IFormFile> albumPictures);
		Task UpdateAsync(Guid guid,  AlbumLogic album, List<Guid> tags, IFormFile albumCoverFile, List<IFormFile> albumPictures);
		Task DeleteAsync(Guid guid);
	}
}
