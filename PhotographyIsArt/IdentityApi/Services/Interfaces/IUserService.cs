using IdentityApi.Domain.Entities;
using WebApiCore.Logic.Base.Interfaces;

namespace IdentityApi.Services.Interfaces
{
	public interface IUserService : IService<User>
	{
		Task CheckUserOnNotExisting(User user);
		Task CheckUser(User user);
		Task UpdateAsync(User user, Guid id, IFormFile image);
	}
}
