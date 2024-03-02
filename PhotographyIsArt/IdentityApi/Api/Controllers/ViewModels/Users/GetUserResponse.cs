using IdentityApi.Domain.Enums;

namespace IdentityApi.Api.Controllers.ViewModels.Users
{
	public class GetUserResponse
	{
		public string? Name { get; set; }
		public string? Login { get; set; }
		public Gender Gender { get; set; }
		public string? Email { get; set; }
		public string? Phone { get; set; }
		public string? ProfilePictureUrl { get; set; }
		public string? UserInfo { get; set; }
	}
}
