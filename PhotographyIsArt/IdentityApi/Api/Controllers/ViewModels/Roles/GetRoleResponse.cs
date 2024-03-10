namespace IdentityApi.Api.Controllers.ViewModels.Roles
{
	public class GetRoleResponse
	{
		public required Guid Id { get; set; }
		public required string Name { get; set; }
		public required bool IsDefault { get; set; }
	}
}
