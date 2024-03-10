using IdentityApi.Domain.Entities;
using WebApiCore.Dal.Base.Interfaces;

namespace IdentityApi.Domain.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> FindUserByLoginAsync(string login);
		Task<User?> FindUserByEmailAsync(string email);
		Task<User?> FindUserByPhoneAsync(string phone);
	}
}
