namespace IdentityApi.Api.Controllers.ViewModels.Users
{
	public class GetShortUserResponse
	{
		public required Guid Id { get; set; }
		public required string Name { get; set; }
		public required string Login { get; set; }
		public required string ProfilePictureUrl { get; set; }
	}
}
