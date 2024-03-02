using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Services.Interfaces;
using WebApiCore.Dal.Base.Interfaces;
using WebApiCore.Dal.Constants;
using WebApiCore.Logic.Base.Interfaces;
using WebApiCore.Logic.Base.Services;

namespace IdentityApi.Services.Implementations
{
	public class UserService : BaseService<User>, IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IImageService _imageService;
		private readonly IWebHostEnvironment _environment;
		public UserService(IUserRepository repository, IImageService imageService, IWebHostEnvironment environment) : base(repository)
		{
			_userRepository = repository;
			_imageService = imageService;
			_environment = environment;
		}

		public override async Task<Guid> AddAsync(User entity)
		{
			await CheckUserOnNotExisting(entity);
			return await _repository.AddAsync(entity);
		}

		public async Task CheckUserOnNotExisting(User user)
		{
			var entity = await _userRepository.FindUserByLogin(user.Login);
			if (entity is not null) throw new Exception("Пользователь с таким логином уже существует!");
			entity = await _userRepository.FindUserByEmail(user.Email);
			if (entity is not null) throw new Exception("Пользователь с таким почтовым адресом уже существует!");
		}

		public async Task CheckUser(User user)
		{
			var entity = await _userRepository.FindUserByEmail(user.Email);
			if (entity is null) throw new Exception("Пользователя с таким почтовым адресом не существует!");
			if (user.Password != entity.Password) throw new Exception("Неверный пароль!");
		}

		public async Task UpdateAsync(User user, Guid id, IFormFile image)
		{
			var entity = await _repository.GetByGuidAsync(id);
			await CheckUserOnNotExisting(user);
			var path = _imageService.CreateName(image.FileName);
			_imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.UserProfilePicturesDirectoryName, entity.ProfilePictureUrl);
			entity.Name = user.Name;
			entity.Login = user.Login;
			entity.Gender = user.Gender;
			entity.Email = user.Email;
			entity.UserInfo = user.UserInfo;
			entity.Password = user.Password;
			entity.ProfilePictureUrl = path;
			await _imageService.SaveFileAsync(image, _environment.ContentRootPath,
				SpecialConstants.UserProfilePicturesDirectoryName, path);
			await _repository.UpdateAsync();
		}
	}
}
