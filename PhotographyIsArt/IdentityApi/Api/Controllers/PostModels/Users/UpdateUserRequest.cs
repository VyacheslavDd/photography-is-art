using IdentityApi.Domain.Enums;

namespace IdentityApi.Api.Controllers.PostModels.Users
{
	public class UpdateUserRequest
	{
		public string? Name { get; set; }
		public string? Login { get; set; }
		public Gender Gender { get; set; }
		public string? Email { get; set; }
		public IFormFile ProfilePicture { get; set; }
		public string? UserInfo { get; set; }
		public string? Password { get; set; }
	}
}
