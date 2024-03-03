using IdentityApi.Api.Controllers.ViewModels.Roles;
using IdentityApi.Domain.Entities;
using IdentityApi.Domain.Enums;

namespace IdentityApi.Api.Controllers.ViewModels.Users
{
	public class GetUserResponse
	{
		public required string Name { get; set; }
		public required string Login { get; set; }
		public required Gender Gender { get; set; }
		public required DateTime BirthDate { get; set; }
		public required string Email { get; set; }
		public required string Phone { get; set; }
		public required string ProfilePictureUrl { get; set; }
		public required string UserInfo { get; set; }
		public required List<GetRoleResponse> Roles { get; set; }
	}
}
