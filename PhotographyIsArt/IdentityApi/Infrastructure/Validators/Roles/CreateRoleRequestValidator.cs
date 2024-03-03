using FluentValidation;
using IdentityApi.Api.Controllers.PostModels.Roles;

namespace IdentityApi.Infrastructure.Validators.Roles
{
	public class CreateRoleRequestValidator : AbstractValidator<CreateRoleRequest>
	{
		public CreateRoleRequestValidator()
		{
			RuleFor(r => r.Name).NotNull().NotEmpty().MinimumLength(3).MaximumLength(20);
		}
	}
}
