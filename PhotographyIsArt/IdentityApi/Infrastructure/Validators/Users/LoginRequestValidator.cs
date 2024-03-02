using FluentValidation;
using IdentityApi.Api.Controllers.PostModels.Users;

namespace IdentityApi.Infrastructure.Validators.Users
{
	public class LoginRequestValidator : AbstractValidator<LoginRequest>
	{
		public LoginRequestValidator()
		{
			RuleFor(u => u.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
			RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(10).MaximumLength(20);
		}
	}
}
