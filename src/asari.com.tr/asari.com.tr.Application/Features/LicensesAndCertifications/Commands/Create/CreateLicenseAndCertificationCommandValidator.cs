using FluentValidation;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Create;

public class CreateLicenseAndCertificationCommandValidator : AbstractValidator<CreateLicenseAndCertificationCommand>
{
    public CreateLicenseAndCertificationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Name).NotEmpty().WithMessage("Lisans ve Sertifika Adlarını boş bırakmayınız");
        RuleFor(x => x.IssuingOrganization).NotEmpty().WithMessage("Lisans ve Sertifika Veren Organizasyonu boş bırakmayınız");
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Name).MaximumLength(350).WithMessage("Lisans ve Sertifika Adı 350 karakterden uzun olamaz.");
        RuleFor(x => x.IssuingOrganization).MaximumLength(250).WithMessage("Lisans ve Sertifika Veren Organizasyonu 250 karakterden uzun olamaz.");
        #endregion
    }
}