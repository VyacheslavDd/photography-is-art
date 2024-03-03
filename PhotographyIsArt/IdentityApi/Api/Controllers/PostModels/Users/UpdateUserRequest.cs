using IdentityApi.Domain.Enums;

namespace IdentityApi.Api.Controllers.PostModels.Users
{
	public class UpdateUserRequest
	{
		public required string Name { get; set; }
		public required string Login { get; set; }
		public required Gender Gender { get; set; }
		public required DateTime BirthDate { get; set; }
		public required string Email { get; set; }
		public required string Phone { get; set; }
		public required IFormFile ProfilePicture { get; set; }
		public required string UserInfo { get; set; }
		public required string Password { get; set; }
	}
}
