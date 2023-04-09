using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Delete;

public class DeleteLicenseAndCertificationSkillCommandValidator : AbstractValidator<DeleteLicenseAndCertificationSkillCommand>
{
    public DeleteLicenseAndCertificationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(LicenseAndCertificationSkillMessages.IdBosOlmamali);
        #endregion
    }
}