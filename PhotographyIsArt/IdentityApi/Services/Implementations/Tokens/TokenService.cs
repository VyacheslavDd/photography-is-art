using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Services.Interfaces.Tokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApiCore.Dal.Constants;

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
				Expires = DateTime.Now.AddMinutes(SpecialConstants.TokenExpireMinutes).ToUniversalTime(),
				UserId = new Guid(),
				User = null
			};
		}

		public async Task<User> GetUserByTokenAsync(string token)
		{
			return await _tokenRepository.GetUserByTokenAsync(token);
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
	}
}
