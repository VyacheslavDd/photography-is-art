using IdentityApi.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApi.Services.Interfaces.Tokens
{
	public interface ITokenService
	{
		string CreateToken(User user);
		RefreshToken GenerateRefreshToken();
		void SetRefreshToken(RefreshToken refreshToken, HttpResponse response);
		Task<User> GetUserByTokenAsync(string token);
		Task RemoveTokenByUserIdAsync(Guid userId);
		Task<string> TokenSetupAsync(User user, HttpResponse response);
		void RefreshTokenCheck(string token, User user);
	}
}
