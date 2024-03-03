using WebApiCore.Dal.Base.Models;

namespace IdentityApi.Domain.Entities
{
    public record Role : BaseModel<Guid>
    {
        public required string Name { get; set; }
        public required bool IsDefault { get; set; }
        public required List<User> Users { get; set; }
    }
}
