using IdentityApi.Domain.Entities;
using WebApiCore.Logic.Base.Interfaces;

namespace IdentityApi.Services.Interfaces.Users
{
    public interface IUserService : IService<User>
    {
        Task UpdateAsync(User user, Guid id, IFormFile image);
    }
}
