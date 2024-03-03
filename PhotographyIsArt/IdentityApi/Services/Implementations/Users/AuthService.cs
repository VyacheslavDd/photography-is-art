using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Services.Interfaces.Roles;
using IdentityApi.Services.Interfaces.Users;
using WebApiCore.Logic.Base.Services;

namespace IdentityApi.Services.Implementations.Users
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;

		public AuthService(IUserRepository userRepository, IUserService userService, IRoleService roleService)
		{
			_userRepository = userRepository;
			_userService = userService;
			_roleService = roleService;
		}

		public async Task<Guid> RegisterUserAsync(User user)
		{
			await CheckUserNonExistenceAsync(user);
			var defaultRole = await _roleService.GetDefaultRoleAsync();
			if (defaultRole is not null) 
			{ 
				user.Roles.Add(defaultRole);
				defaultRole.Users.Add(user);
			}

			return await _userService.AddAsync(user);
		}

		public async Task CheckUserLoginInputAsync(User user)
		{
			var entity = await _userRepository.FindUserByEmailAsync(user.Email);
			if (entity is null) throw new Exception("Пользователя с таким почтовым адресом не существует!");
			if (!BCrypt.Net.BCrypt.Verify(user.Password, entity.Password)) throw new Exception("Неверный пароль!");
		}

		public async Task CheckUserNonExistenceAsync(User user)
		{
			var entity = await _userRepository.FindUserByLoginAsync(user.Login);
			if (entity is not null) throw new Exception("Пользователь с таким логином уже существует!");
			entity = await _userRepository.FindUserByEmailAsync(user.Email);
			if (entity is not null) throw new Exception("Пользователь с таким почтовым адресом уже существует!");
			entity = await _userRepository.FindUserByPhoneAsync(user.Phone);
			if (entity is not null) throw new Exception("Пользователь с таким телефоном уже существует!");
		}
	}
}
