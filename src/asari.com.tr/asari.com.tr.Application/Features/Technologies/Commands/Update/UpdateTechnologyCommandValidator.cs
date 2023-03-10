using FluentValidation;

namespace asari.com.tr.Application.Features.Technologies.Commands.Update;

public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
{
    public UpdateTechnologyCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Teknoloji Id'si boş bırakmayınız");
        RuleFor(x => x.Title).NotEmpty().WithMessage("Teknoloji Adını boş bırakmayınız");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Teknoloji Açıklamasını boş bırakmayınız");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Teknoloji İçeriğini boş bırakmayınız");
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Title).MaximumLength(250).WithMessage("Teknoloji Adı 250 karakterden uzun olamaz.");
        #endregion
    }
}