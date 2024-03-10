using AlbumApi.Dal.Albums.Interfaces;
using AlbumApi.Logic.Albums.Interfaces;
using AlbumApi.Logic.Albums.Models;
using AlbumApi.Dal.Albums.Models;
using AlbumApi.Logic.Tags.Interfaces;
using WebApiCore.Dal.Base.Interfaces;
using AutoMapper;
using AlbumApi.Logic.Tags.Models;
using AlbumApi.Dal.Tags.Models;
using AlbumApi.Dal.Tags.Interfaces;
using WebApiCore.Logic.Base.Interfaces;
using WebApiCore.Dal.Constants;
using WebApiCore.Helpers;
using WebApiCore.Libs.AlbumUserConnectionService.Interfaces;
using WebApiCore.Libs.AlbumUserConnectionService.Models.Requests;

namespace AlbumApi.Logic.Albums
{
	public class AlbumService : IAlbumService
	{
		private readonly IRepository<AlbumDal> _albumRepository;
		private readonly IMapper _mapper;
		private readonly IRepository<AlbumTagDal> _albumTagRepository;
		private readonly IImageService _imageService;
		private readonly IAlbumUserConnectionService _albumUserConnectionService;
		private readonly IWebHostEnvironment _environment;

		public AlbumService(IRepository<AlbumDal> albumRepository, IRepository<AlbumTagDal> albumTagRepository, IMapper mapper,
			IWebHostEnvironment environment, IImageService imageService, IAlbumUserConnectionService albumUserConnectionService)
		{
			_albumRepository = albumRepository;
			_albumTagRepository = albumTagRepository;
			_mapper = mapper;
			_environment = environment;
			_imageService = imageService;
			_albumUserConnectionService = albumUserConnectionService;
		}

		public async Task<Guid> AddAsync(AlbumLogic album, List<Guid> tags, IFormFile albumCoverFile, List<IFormFile> albumPictures)
		{
			await _albumUserConnectionService.CheckUserExistenseAsync(new CheckUserExistenceRequest() { UserId = album.UserId });
			var path = _imageService.CreateName(albumCoverFile.FileName);
			var dalModel = _mapper.Map<AlbumDal>(album);
			await MatchTagsAsync(dalModel, tags);
			dalModel.AlbumCoverImageUrl = path;
			dalModel.AlbumPicturesUrls = await AlbumServiceHelper.SavePictures(albumPictures, _imageService,
				_environment.ContentRootPath, SpecialConstants.AlbumPicturesDirectoryName);
			await _imageService.SaveFileAsync(albumCoverFile, _environment.ContentRootPath, SpecialConstants.AlbumCoversDirectoryName, path);
			return await _albumRepository.AddAsync(dalModel);
		}

		public async Task DeleteAsync(Guid guid)
		{
			var entity = await GetByGuidAsync(guid);
			_imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.AlbumCoversDirectoryName, entity.AlbumCoverImageUrl);
			AlbumServiceHelper.DeletePictures(entity.AlbumPicturesUrls, _imageService,
				_environment.ContentRootPath, SpecialConstants.AlbumPicturesDirectoryName);
			await _albumRepository.DeleteAsync(guid);
		}

		public async Task<List<AlbumLogic>> GetAllAsync()
		{
			var data = await _albumRepository.GetAllAsync();
			var mapped = _mapper.Map<List<AlbumLogic>>(data);
			return mapped;
		}

		public async Task<AlbumLogic> GetByGuidAsync(Guid guid)
		{
			var entity = await _albumRepository.GetByGuidAsync(guid);
			var mapped = _mapper.Map<AlbumLogic>(entity);
			mapped.Tags = _mapper.Map<List<AlbumTagLogic>>(entity.Tags);
			return mapped;
		}

		public async Task UpdateAsync(Guid guid, AlbumLogic album, List<Guid> tags, IFormFile albumCoverFile, List<IFormFile> albumPictures)
		{
			var path = _imageService.CreateName(albumCoverFile.FileName);
			var entity = await _albumRepository.GetByGuidAsync(guid);
			await MatchTagsAsync(entity, tags);
			entity.Title = album.Title;
			entity.Description = album.Description;
			entity.LastRedactedDate = album.LastRedactedDate;
			_imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.AlbumCoversDirectoryName, entity.AlbumCoverImageUrl);
			AlbumServiceHelper.DeletePictures(entity.AlbumPicturesUrls, _imageService,
				_environment.ContentRootPath, SpecialConstants.AlbumPicturesDirectoryName);
			entity.AlbumCoverImageUrl = path;
			entity.AlbumPicturesUrls = await AlbumServiceHelper.SavePictures(albumPictures, _imageService,
				_environment.ContentRootPath, SpecialConstants.AlbumPicturesDirectoryName);
			await _imageService.SaveFileAsync(albumCoverFile, _environment.ContentRootPath, SpecialConstants.AlbumCoversDirectoryName, path);
			await _albumRepository.UpdateAsync();
		}

		private async Task MatchTagsAsync(AlbumDal album, List<Guid> tags)
		{
			if (tags is null) return;
			album.Tags = new List<AlbumTagDal>();
			var selectedTags = new List<AlbumTagDal>();
			foreach (var tag in tags)
			{
				var entity = await _albumTagRepository.GetByGuidAsync(tag) ?? throw new Exception("Один из указанных тегов не существует!");
				selectedTags.Add(entity);
			}
			foreach (var tag in selectedTags)
			{
				album.Tags.Add(tag);
				tag.Albums.Add(album);
			}
		}
	}
}
