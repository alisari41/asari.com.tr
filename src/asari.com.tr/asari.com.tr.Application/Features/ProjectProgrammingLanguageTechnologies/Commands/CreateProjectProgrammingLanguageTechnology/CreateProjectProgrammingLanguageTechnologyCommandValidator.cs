using FluentValidation;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.CreateProjectProgrammingLanguageTechnology;

public class CreateProjectProgrammingLanguageTechnologyCommandValidator : AbstractValidator<CreateProjectProgrammingLanguageTechnologyCommand>
{
    // FluentValidation ile Format Doğrulama işlemleri
    // Ekeleme işlemleri için ayrı güncelleme işlemleri vs. ler için ayrı doğrulama işlemleri olabileceği için "CreateBrandCommand" ile ekleme işlemleri için yapıldı

    public CreateProjectProgrammingLanguageTechnologyCommandValidator()
	{
        #region Zorunlu Alanlar
        RuleFor(x => x.ProgrammingLanguageTechnologyId).NotEmpty().WithMessage("Programlama Dili Id'si Boş olamaz");
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Proje Id'si boş bırakmayınız");
        #endregion
    }
}
