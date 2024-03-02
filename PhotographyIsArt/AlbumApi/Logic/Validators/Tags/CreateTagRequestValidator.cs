using AlbumApi.Api.Controllers.Tags.RequestModels;
using FluentValidation;

namespace AlbumApi.Logic.Validators.Tags
{
	public class CreateTagRequestValidator : AbstractValidator<CreateTagRequest>
	{
		public CreateTagRequestValidator()
		{
			RuleFor(t => t.Name).NotNull().NotEmpty().MinimumLength(5).MaximumLength(30);
		}
	}
}
