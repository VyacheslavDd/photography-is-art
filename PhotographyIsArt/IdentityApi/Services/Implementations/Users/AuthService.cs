using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Services.Interfaces.Roles;
using IdentityApi.Services.Interfaces.Tokens;
using IdentityApi.Services.Interfaces.Users;
using WebApiCore.Exceptions;
using WebApiCore.Exceptions.Enums;
using WebApiCore.Logic.Base.Services;

namespace IdentityApi.Services.Implementations.Users
{
	public class AuthService : IAuthService
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserService _userService;
		private readonly IRoleService _roleService;
		private readonly ITokenService _tokenService;

		public AuthService(IUserRepository userRepository, IUserService userService, IRoleService roleService, ITokenService tokenService)
		{
			_userRepository = userRepository;
			_userService = userService;
			_roleService = roleService;
			_tokenService = tokenService;
		}

		//регистрация + добавление дефолтной роли (если существует)
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

		public async Task<string> UserLoginAsync(User user, HttpResponse response)
		{
			var entity = await _userRepository.FindUserByEmailAsync(user.Email);
			if (entity is null) ExceptionHandler.ThrowException(ExceptionType.ArgumentNull, "Пользователя с таким почтовым адресом не существует!");
			if (!BCrypt.Net.BCrypt.Verify(user.Password, entity.Password)) ExceptionHandler.ThrowException(ExceptionType.IncorrectPassword,
				"Неверный пароль!");
			return await TokenSetupAsync(entity, response);
		}

		public async Task<string> TokenSetupAsync(User user, HttpResponse response)
		{
			var token = _tokenService.CreateToken(user);
			var refreshToken = _tokenService.GenerateRefreshToken();
			_tokenService.SetRefreshToken(refreshToken, response);
			await _tokenService.RemoveTokenByUserIdAsync(user.Id);
			user.Token = refreshToken;
			await _userRepository.UpdateAsync();
			return token;
		}

		public async Task CheckUserNonExistenceAsync(User user)
		{
			var entity = await _userRepository.FindUserByLoginAsync(user.Login);
			if (entity is not null) ExceptionHandler.ThrowException(ExceptionType.UserAlreadyExists, "Пользователь с таким логином уже существует!");
			entity = await _userRepository.FindUserByEmailAsync(user.Email);
			if (entity is not null) ExceptionHandler.ThrowException(ExceptionType.UserAlreadyExists, "Пользователь с таким почтовым адресом уже существует!");
			entity = await _userRepository.FindUserByPhoneAsync(user.Phone);
			if (entity is not null) ExceptionHandler.ThrowException(ExceptionType.UserAlreadyExists, "Пользователь с таким телефоном уже существует!");
		}
	}
}
