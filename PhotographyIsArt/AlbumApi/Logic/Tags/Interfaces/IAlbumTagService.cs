using AlbumApi.Logic.Albums.Models;
using AlbumApi.Logic.Tags.Models;

namespace AlbumApi.Logic.Tags.Interfaces
{
	public interface IAlbumTagService
	{
		Task<List<AlbumTagLogic>> GetAllAsync();
		Task<AlbumTagLogic> GetByGuidAsync(Guid guid);
		Task<Guid> AddAsync(AlbumTagLogic tag);
		Task UpdateAsync(Guid guid, AlbumTagLogic tag);
		Task DeleteAsync(Guid guid);
	}
}
