using asari.com.tr.Application.Features.Projects.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Projects.Commands.Create;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    // FluentValidation ile Format Doğrulama işlemleri
    // Ekeleme işlemleri için ayrı güncelleme işlemleri vs. ler için ayrı doğrulama işlemleri olabileceği için "CreateBrandCommand" ile ekleme işlemleri için yapıldı

    public CreateProjectCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Title).NotEmpty().WithMessage(ProjectMessages.TitleBosOlmamali);
        RuleFor(x => x.Description).NotEmpty().WithMessage(ProjectMessages.DescriptionBosOlmamali);
        RuleFor(x => x.Content).NotEmpty().WithMessage(ProjectMessages.ContentBosOlmamali);
        RuleFor(x => x.CreateDate).NotEmpty().WithMessage(ProjectMessages.CreateDateBosOlmamali);
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Title).MaximumLength(250).WithMessage(ProjectMessages.TitleMaxKarakter);
        #endregion
    }
}