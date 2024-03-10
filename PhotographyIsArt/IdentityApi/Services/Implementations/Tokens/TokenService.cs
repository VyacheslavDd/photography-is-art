using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Infrastructure.Repositories;
using IdentityApi.Services.Interfaces.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiCore.Dal.Constants;
using WebApiCore.Exceptions;
using WebApiCore.Exceptions.Enums;

namespace IdentityApi.Services.Implementations.Tokens
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _config;
		private readonly ITokenRepository _tokenRepository;

		public TokenService(IConfiguration config, ITokenRepository tokenRepository)
		{
			_config = config;
			_tokenRepository = tokenRepository;
		}

		public string CreateToken(User user)
		{
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.Name, user.Login)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection(SpecialConstants.TokenSectionFullName).Value));
			var credentials = new SigningCredentials(key, SecurityAlgorithms.Aes128CbcHmacSha256);
			var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddMinutes(SpecialConstants.TokenExpireMinutes), signingCredentials: credentials);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public RefreshToken GenerateRefreshToken()
		{
			return new RefreshToken()
			{
				Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
				Created = DateTime.Now.ToUniversalTime(),
				Expires = DateTime.Now.AddMinutes(SpecialConstants.TokenExpireMinutes*5).ToUniversalTime(),
				UserId = new Guid(),
				User = null
			};
		}

		public async Task<User> GetUserByTokenAsync(string token)
		{
			return await _tokenRepository.GetUserByTokenAsync(token);
		}

		public void RefreshTokenCheck(string refreshToken, User user)
		{
			if (user is null || !user.Token.Token.Equals(refreshToken)) ExceptionHandler.ThrowException(ExceptionType.InvalidToken, "Некорректный токен!");
			if (user.Token.Expires < DateTime.Now.ToUniversalTime()) ExceptionHandler.ThrowException(ExceptionType.TokenExpired,
				"Время действия токена истекло.");
		}

		public async Task RemoveTokenByUserIdAsync(Guid userId)
		{
			try
			{
				await _tokenRepository.RemoveTokenByUserIdAsync(userId);
			}
			catch
			{
				return;
			}
		}

		public void SetRefreshToken(RefreshToken refreshToken, HttpResponse response)
		{
			var cookies = new CookieOptions()
			{
				HttpOnly = true,
				Expires = refreshToken.Expires
			};
			response.Cookies.Append(SpecialConstants.RefreshTokenCookieName, refreshToken.Token, cookies);
		}

		public async Task<string> TokenSetupAsync(User user, HttpResponse response)
		{
			var token = CreateToken(user);
			var refreshToken = GenerateRefreshToken();
			SetRefreshToken(refreshToken, response);
			await RemoveTokenByUserIdAsync(user.Id);
			user.Token = refreshToken;
			await _tokenRepository.UpdateAsync();
			return token;
		}
	}
}
