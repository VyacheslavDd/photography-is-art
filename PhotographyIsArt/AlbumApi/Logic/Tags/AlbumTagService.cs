using AlbumApi.Dal.Albums.Interfaces;
using AlbumApi.Dal.Tags.Interfaces;
using AlbumApi.Logic.Tags.Interfaces;
using AlbumApi.Logic.Tags.Models;
using AlbumApi.Dal.Tags;
using AlbumApi.Dal.Albums;
using AlbumApi.Dal.Tags.Models;
using AlbumApi.Logic.Albums.Models;
using WebApiCore.Dal.Base.Interfaces;
using AutoMapper;

namespace AlbumApi.Logic.Tags
{
	public class AlbumTagService : IAlbumTagService
	{
		private readonly IRepository<AlbumTagDal> _albumTagRepository;
		private readonly IMapper _mapper;

		public AlbumTagService(IRepository<AlbumTagDal> albumTagRepository, IMapper mapper)
		{
			_albumTagRepository = albumTagRepository;
			_mapper = mapper;
		}

		public async Task<Guid> AddAsync(AlbumTagLogic tag)
		{
			var tags = await GetAllAsync();
			if (tags.Any(t => t.Name.ToLower() == tag.Name.ToLower())) throw new Exception("Неуникальное название тега!");
			var modelDal = _mapper.Map<AlbumTagDal>(tag);
			return await _albumTagRepository.AddAsync(modelDal);
		}

		public async Task DeleteAsync(Guid guid)
		{
			await _albumTagRepository.DeleteAsync(guid);
		}

		public async Task<List<AlbumTagLogic>> GetAllAsync()
		{
			var tags = await _albumTagRepository.GetAllAsync();
			var mapped = _mapper.Map<List<AlbumTagLogic>>(tags);
			return mapped;
		}

		public async Task<AlbumTagLogic> GetByGuidAsync(Guid guid)
		{
			var tag = await _albumTagRepository.GetByGuidAsync(guid);
			var mappedTag = _mapper.Map<AlbumTagLogic>(tag);
			mappedTag.Albums = _mapper.Map<List<AlbumLogic>>(tag.Albums);
			return mappedTag;
		}

		public async Task UpdateAsync(Guid guid, AlbumTagLogic tag)
		{
			var entity = await _albumTagRepository.GetByGuidAsync(guid);
			entity.Name = tag.Name;
			await _albumTagRepository.UpdateAsync();
		}
	}
}
