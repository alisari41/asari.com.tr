using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Create;

public class CreateProjectProgrammingLanguageTechnologyCommandValidator : AbstractValidator<CreateProjectProgrammingLanguageTechnologyCommand>
{
    // FluentValidation ile Format Doğrulama işlemleri
    // Ekeleme işlemleri için ayrı güncelleme işlemleri vs. ler için ayrı doğrulama işlemleri olabileceği için "CreateBrandCommand" ile ekleme işlemleri için yapıldı

    public CreateProjectProgrammingLanguageTechnologyCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.ProgrammingLanguageTechnologyId).NotEmpty().WithMessage(ProjectProgrammingLanguageTechnologyMessages.ProgrammingLanguageTechnologyIdBosOlmamali);
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage(ProjectProgrammingLanguageTechnologyMessages.ProjectIdBosOlmamali);
        #endregion
    }
}
