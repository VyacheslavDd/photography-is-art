namespace IdentityApi.Api.Controllers.ViewModels.Users
{
	public class GetUsersResponse
	{
		public Guid Id { get; set; }
		public string? Name { get; set; }
		public string? Login { get; set; }
		public string? ProfilePictureUrl { get; set; }
	}
}
