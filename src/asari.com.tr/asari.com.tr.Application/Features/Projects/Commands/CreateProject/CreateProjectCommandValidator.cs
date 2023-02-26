using FluentValidation;

namespace asari.com.tr.Application.Features.Projects.Commands.CreateProject;

public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
{
    // FluentValidation ile Format Doğrulama işlemleri
    // Ekeleme işlemleri için ayrı güncelleme işlemleri vs. ler için ayrı doğrulama işlemleri olabileceği için "CreateBrandCommand" ile ekleme işlemleri için yapıldı

    public CreateProjectCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Title).NotEmpty().WithMessage("Proje Adını boş bırakmayınız");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Proje Açıklamasını boş bırakmayınız");
        RuleFor(x => x.Content).NotEmpty().WithMessage("Proje İçeriğini boş bırakmayınız");
        RuleFor(x => x.CreateDate).NotEmpty().WithMessage("Proje Tarihini boş bırakmayınız");
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Title).MaximumLength(250).WithMessage("Proje Adı 250 karakterden uzun olamaz.");
        #endregion
    }
}