using IdentityApi.Domain.Entities;
using WebApiCore.Dal.Base.Interfaces;

namespace IdentityApi.Domain.Interfaces
{
	public interface IUserRepository : IRepository<User>
	{
		Task<User?> FindUserByLogin(string login);
		Task<User?> FindUserByEmail(string email);
		Task<User?> FindUserByPhone(string phone);
	}
}
