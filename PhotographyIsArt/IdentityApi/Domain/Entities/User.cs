using IdentityApi.Domain.Enums;
using WebApiCore.Dal.Base;
using WebApiCore.Dal.Base.Models;

namespace IdentityApi.Domain.Entities
{
	public record User : BaseModel<Guid>
	{
		public required string Name { get; set; }
		public required string Login { get; set; }
		public required Gender Gender { get; set; }
		public required DateTime BirthDate { get; set; }
		public required string Email { get; set; }
		public required string Phone { get; set; }
		public string ProfilePictureUrl { get; set; }
		public string UserInfo { get; set; }
		public required string Password { get; set; }
		public required List<Role> Roles { get; set; }
	}
}
