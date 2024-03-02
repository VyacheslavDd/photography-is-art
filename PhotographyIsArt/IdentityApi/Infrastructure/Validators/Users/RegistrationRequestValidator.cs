using FluentValidation;
using IdentityApi.Api.Controllers.PostModels.Users;

namespace IdentityApi.Infrastructure.Validators.Users
{
	public class RegistrationRequestValidator : AbstractValidator<RegistrationRequest>
	{
		public RegistrationRequestValidator()
		{
			RuleFor(u => u.Name).NotNull().NotEmpty().MinimumLength(2).MaximumLength(40);
			RuleFor(u => u.Login).NotNull().NotEmpty().MinimumLength(8).MaximumLength(20);
			RuleFor(u => u.Gender).IsInEnum();
			RuleFor(u => u.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
			RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(10).MaximumLength(20);
		}
	}
}
