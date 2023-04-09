using asari.com.tr.Application.Features.LicensesAndCertifications.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Delete;

public class DeleteLicenseAndCertificationCommandValidator : AbstractValidator<DeleteLicenseAndCertificationCommand>
{
    public DeleteLicenseAndCertificationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(LicensesAndCertificationMessages.IdBosOlmamali);
        #endregion
    }
}