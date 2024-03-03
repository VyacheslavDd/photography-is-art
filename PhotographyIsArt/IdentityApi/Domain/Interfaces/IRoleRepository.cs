using IdentityApi.Domain.Entities;
using WebApiCore.Dal.Base.Interfaces;

namespace IdentityApi.Domain.Interfaces
{
	public interface IRoleRepository : IRepository<Role>
	{
		Task<Role> FindRoleByNameAsync(string roleName);
		Task<Role> FindDefaultRoleAsync();
	}
}
