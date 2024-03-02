using IdentityApi.Domain.Enums;
using WebApiCore.Dal.Base;
using WebApiCore.Dal.Base.Models;

namespace IdentityApi.Domain.Entities
{
	public record User : BaseModel<Guid>
	{
		public string? Name { get; set; }
		public string? Login { get; set; }
		public Gender Gender { get; set; }
		public string? Email { get; set; }
		public string? ProfilePictureUrl { get; set; }
		public string? UserInfo { get; set; }
		public string? Password { get; set; }
	}
}
