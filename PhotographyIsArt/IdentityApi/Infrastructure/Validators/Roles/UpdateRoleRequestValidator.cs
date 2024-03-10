using FluentValidation;
using IdentityApi.Api.Controllers.PostModels.Roles;

namespace IdentityApi.Infrastructure.Validators.Roles
{
	public class UpdateRoleRequestValidator : AbstractValidator<UpdateRoleRequest>
	{
		public UpdateRoleRequestValidator()
		{
			RuleFor(r => r.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(20);
		}
	}
}
