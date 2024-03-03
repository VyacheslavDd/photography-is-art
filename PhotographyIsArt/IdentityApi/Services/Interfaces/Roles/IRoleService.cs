using IdentityApi.Domain.Entities;
using WebApiCore.Logic.Base.Interfaces;

namespace IdentityApi.Services.Interfaces.Roles
{
	public interface IRoleService : IService<Role>
	{
		Task ValidateRoleAsync(Role role);
		Task<Role> GetDefaultRoleAsync();
		Task UpdateAsync(Role role, Guid id);
		Task AssignRoleToUserAsync(Guid roleId, Guid userId);
		Task RemoveRoleFromUserAsync(Guid roleId, Guid userId);
	}
}
