using AlbumApi.Api.Controllers.Albums.RequestModels;
using FluentValidation;

namespace AlbumApi.Logic.Validators.Albums
{
    public class CreateAlbumRequestValidator : AbstractValidator<CreateAlbumRequest>
    {
        public CreateAlbumRequestValidator()
        {
            RuleFor(a => a.Title).NotNull().NotEmpty().MinimumLength(5).MaximumLength(70);
            RuleFor(a => a.Description).NotNull().NotEmpty().MinimumLength(5).MaximumLength(500);
            RuleFor(a => a.AlbumPictures).Must(HaveEnoughPictures).WithMessage("Альбом должен иметь от 3 до 30 картинок!");
        }

        private bool HaveEnoughPictures(List<IFormFile> pictures)
        {
            return pictures.Count > 3 && pictures.Count < 30;
        }
    }
}
