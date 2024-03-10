using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WebApiCore.Dal.Base.Repositories;

namespace IdentityApi.Infrastructure.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		private readonly UsersDbContext _usersContext;
		public UserRepository(UsersDbContext context) : base(context.Users, context)
		{
			_usersContext = context;
		}

		public override async Task<User?> GetByGuidAsync(Guid guid)
		{
			return await _usersContext.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == guid);
		}

		public async Task<User?> FindUserByEmailAsync(string email)
		{
			return await _usersContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());
		}

		public async Task<User?> FindUserByLoginAsync(string login)
		{
			return await _usersContext.Users.FirstOrDefaultAsync(u => u.Login.ToLower() == login.ToLower());
		}

		public async Task<User?> FindUserByPhoneAsync(string phone)
		{
			return await _usersContext.Users.FirstOrDefaultAsync(u => u.Phone == phone);
		}
	}
}
