using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace IdentityApi.Infrastructure.Repositories
{
	public class TokenRepository : ITokenRepository
	{
		private readonly UsersDbContext _context;

		public TokenRepository(UsersDbContext context)
		{
			_context = context;
		}

		public async Task<RefreshToken> GetTokenByUserIdAsync(Guid userId)
		{
			return await _context.Tokens.FirstOrDefaultAsync(t => t.UserId == userId);
		}

		public async Task<User> GetUserByTokenAsync(string token)
		{
			return (await _context.Tokens.Include(t => t.User).FirstOrDefaultAsync(t => t.Token == token))?.User;
		}

		public async Task RemoveTokenByUserIdAsync(Guid userId)
		{
			var token = await GetTokenByUserIdAsync(userId);
			_context.Tokens.Remove(token);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync()
		{
			await _context.SaveChangesAsync();
		}
	}
}
