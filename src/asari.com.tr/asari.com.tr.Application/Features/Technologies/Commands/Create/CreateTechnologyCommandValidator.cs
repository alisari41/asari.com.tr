using asari.com.tr.Application.Features.Technologies.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Technologies.Commands.Create;

public class CreateTechnologyCommandValidator : AbstractValidator<CreateTechnologyCommand>
{
    // FluentValidation ile Format Doğrulama işlemleri
    // Ekeleme işlemleri için ayrı güncelleme işlemleri vs. ler için ayrı doğrulama işlemleri olabileceği için "CreateBrandCommand" ile ekleme işlemleri için yapıldı

    public CreateTechnologyCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Title).NotEmpty().WithMessage(TechnologyMessages.TitleBosOlmamali);
        RuleFor(x => x.Description).NotEmpty().WithMessage(TechnologyMessages.DescriptionBosOlmamali);
        RuleFor(x => x.Content).NotEmpty().WithMessage(TechnologyMessages.ContentBosOlmamali);
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Title).MaximumLength(250).WithMessage(TechnologyMessages.TitleMaxKarakter);
        #endregion
    }
}
