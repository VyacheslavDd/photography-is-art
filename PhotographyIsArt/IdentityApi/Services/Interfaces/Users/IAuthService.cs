using IdentityApi.Domain.Entities;
using WebApiCore.Logic.Base.Interfaces;

namespace IdentityApi.Services.Interfaces.Users
{
    public interface IAuthService
    {
        Task<Guid> RegisterUser(User user);
        Task CheckUserNonExistence(User user);
        Task CheckUserLoginInput(User user);
    }
}
