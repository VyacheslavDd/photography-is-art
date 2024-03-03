namespace IdentityApi.Api.Controllers.PostModels.Users
{
	public class LoginRequest
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}
}
