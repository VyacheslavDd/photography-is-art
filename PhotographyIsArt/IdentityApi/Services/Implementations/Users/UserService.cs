using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Services.Interfaces.Users;
using WebApiCore.Dal.Base.Interfaces;
using WebApiCore.Dal.Constants;
using WebApiCore.Logic.Base.Interfaces;
using WebApiCore.Logic.Base.Services;

namespace IdentityApi.Services.Implementations.Users
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IImageService _imageService;
        private readonly IWebHostEnvironment _environment;
        public UserService(IUserRepository repository, IImageService imageService,
            IWebHostEnvironment environment) : base(repository)
        {
            _userRepository = repository;
            _imageService = imageService;
            _environment = environment;
        }

        public async Task UpdateAsync(User user, Guid id, IFormFile image)
        {
            var entity = await _repository.GetByGuidAsync(id);
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

		public override async Task DeleteAsync(Guid guid)
		{
            var entity = await _repository.GetByGuidAsync(guid);
            _imageService.DeleteFile(_environment.ContentRootPath, SpecialConstants.UserProfilePicturesDirectoryName, entity.ProfilePictureUrl);
            await _repository.DeleteAsync(guid);
		}
	}
}
