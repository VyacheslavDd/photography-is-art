using IdentityApi.Domain.Entities;
using WebApiCore.Logic.Base.Interfaces;

namespace IdentityApi.Services.Interfaces.Users
{
    public interface IAuthService
    {
        Task<Guid> RegisterUserAsync(User user);
        Task CheckUserNonExistenceAsync(User user);
        Task<string> UserLoginAsync(User user, HttpResponse response);
        Task<string> TokenSetupAsync(User user, HttpResponse response);
    }
}
