using IdentityApi.Domain.Entities;

namespace IdentityApi.Domain.Interfaces
{
	public interface ITokenRepository
	{
		Task RemoveTokenByUserIdAsync(Guid userId);
		Task<User> GetUserByTokenAsync(string token);
		Task<RefreshToken> GetTokenByUserIdAsync(Guid userId);
	}
}
