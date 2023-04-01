using FluentValidation;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Delete;

public class DeleteLicenseAndCertificationCommandValidator : AbstractValidator<DeleteLicenseAndCertificationCommand>
{
    public DeleteLicenseAndCertificationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Lisans ve Sertifika Id'si boş bırakmayınız");
        #endregion
    }
}