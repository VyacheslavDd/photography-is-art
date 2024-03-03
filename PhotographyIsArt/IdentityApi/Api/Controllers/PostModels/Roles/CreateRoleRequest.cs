namespace IdentityApi.Api.Controllers.PostModels.Roles
{
	public class CreateRoleRequest
	{
		public required string Name { get; set; }
		public required bool IsDefault { get; set; }
	}
}
