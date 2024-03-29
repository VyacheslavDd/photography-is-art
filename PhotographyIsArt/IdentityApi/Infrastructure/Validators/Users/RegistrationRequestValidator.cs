﻿using FluentValidation;
using IdentityApi.Api.Controllers.PostModels.Users;
using System.Text.RegularExpressions;

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
			RuleFor(u => u.Phone).Matches(new Regex("^(\\+7\\d{10}|8\\d{10})$"));
			RuleFor(u => u.Password).NotNull().NotEmpty().MinimumLength(10).MaximumLength(20);
			RuleFor(u => u.BirthDate).Must(HaveEnoughTimeSpan).WithMessage("Ваш возраст должен быть не менее 14 лет!"); ;
		}

		private bool HaveEnoughTimeSpan(DateTime birthDate)
		{
			var years = DateTime.Now.Year - birthDate.Year;
			return years >= 10;
		}
	}
}
