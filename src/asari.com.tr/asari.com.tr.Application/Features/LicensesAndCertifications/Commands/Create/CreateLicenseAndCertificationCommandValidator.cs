using asari.com.tr.Application.Features.LicensesAndCertifications.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Create;

public class CreateLicenseAndCertificationCommandValidator : AbstractValidator<CreateLicenseAndCertificationCommand>
{
    public CreateLicenseAndCertificationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Name).NotEmpty().WithMessage(LicensesAndCertificationMessages.NameBosOlmamali);
        RuleFor(x => x.IssuingOrganization).NotEmpty().WithMessage(LicensesAndCertificationMessages.IssuingOrganizationBosOlmamali);
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Name).MaximumLength(350).WithMessage(LicensesAndCertificationMessages.NameMaxKarakter);
        RuleFor(x => x.IssuingOrganization).MaximumLength(250).WithMessage(LicensesAndCertificationMessages.IssuingOrganizationMaxKarakter);
        #endregion
    }
}