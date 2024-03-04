using IdentityApi.Domain.Entities;

namespace IdentityApi.Services.Interfaces.Tokens
{
	public interface ITokenService
	{
		string CreateToken(User user);
		RefreshToken GenerateRefreshToken();
		void SetRefreshToken(RefreshToken refreshToken, HttpResponse response);
		Task<User> GetUserByTokenAsync(string token);
		Task RemoveTokenByUserIdAsync(Guid userId);
	}
}
