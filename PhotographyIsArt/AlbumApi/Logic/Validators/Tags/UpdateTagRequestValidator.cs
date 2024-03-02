using AlbumApi.Api.Controllers.Tags.RequestModels;
using FluentValidation;

namespace AlbumApi.Logic.Validators.Tags
{
	public class UpdateTagRequestValidator : AbstractValidator<UpdateTagRequest>
	{
		public UpdateTagRequestValidator()
		{
			RuleFor(t => t.Name).NotNull().NotEmpty().MinimumLength(5).MaximumLength(30);
		}
	}
}
