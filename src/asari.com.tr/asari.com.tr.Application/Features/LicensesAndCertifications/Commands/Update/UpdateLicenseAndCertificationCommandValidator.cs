using FluentValidation;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Update;

public class UpdateLicenseAndCertificationCommandValidator : AbstractValidator<UpdateLicenseAndCertificationCommand>
{
    public UpdateLicenseAndCertificationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Lisans ve Sertifika Id'si boş bırakmayınız");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Lisans ve Sertifika Adlarını boş bırakmayınız");
        RuleFor(x => x.IssuingOrganization).NotEmpty().WithMessage("Lisans ve Sertifika Veren Organizasyonu boş bırakmayınız");
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Name).MaximumLength(350).WithMessage("Lisans ve Sertifika Adı 350 karakterden uzun olamaz.");
        RuleFor(x => x.IssuingOrganization).MaximumLength(250).WithMessage("Lisans ve Sertifika Veren Organizasyonu 250 karakterden uzun olamaz.");
        #endregion
    }
}