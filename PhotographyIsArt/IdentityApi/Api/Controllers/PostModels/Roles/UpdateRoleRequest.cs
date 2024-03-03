namespace IdentityApi.Api.Controllers.PostModels.Roles
{
	public class UpdateRoleRequest
	{
		public required string Name {  get; set; }
		public required bool IsDefault { get; set; }
	}
}
