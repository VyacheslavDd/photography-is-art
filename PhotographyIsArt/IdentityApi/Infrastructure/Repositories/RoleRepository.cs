using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Interfaces;
using IdentityApi.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using WebApiCore.Dal.Base.Repositories;

namespace IdentityApi.Infrastructure.Repositories
{
	public class RoleRepository : BaseRepository<Role>, IRoleRepository
	{
		private readonly UsersDbContext _usersContext;

		public RoleRepository(UsersDbContext context) : base(context.Roles, context)
		{
			_usersContext = context;
		}

		public override async Task<Role?> GetByGuidAsync(Guid guid)
		{
			return await _usersContext.Roles.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == guid);
		}

		public async Task<Role> FindDefaultRoleAsync()
		{
			return await _usersContext.Roles.Include(r => r.Users).FirstOrDefaultAsync(r => r.IsDefault);
		}

		public async Task<Role> FindRoleByNameAsync(string roleName)
		{
			return await _usersContext.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == roleName.ToLower());
		}
	}
}
