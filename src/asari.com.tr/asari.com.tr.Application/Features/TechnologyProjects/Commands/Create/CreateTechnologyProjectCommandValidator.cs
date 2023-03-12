using FluentValidation;

namespace asari.com.tr.Application.Features.TechnologyProjects.Commands.Create;

public class CreateTechnologyProjectCommandValidator : AbstractValidator<CreateTechnologyProjectCommand>
{
    // FluentValidation ile Format Doğrulama işlemleri
    // Ekeleme işlemleri için ayrı güncelleme işlemleri vs. ler için ayrı doğrulama işlemleri olabileceği için "CreateBrandCommand" ile ekleme işlemleri için yapıldı
    public CreateTechnologyProjectCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.TechnologyId).NotEmpty().WithMessage("Teknoloji Id'si Boş olamaz");
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Proje Id'si boş bırakmayınız");
        #endregion
    }
}