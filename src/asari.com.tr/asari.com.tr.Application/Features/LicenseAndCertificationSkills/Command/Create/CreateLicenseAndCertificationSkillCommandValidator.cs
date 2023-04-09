using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Create;

public class CreateLicenseAndCertificationSkillCommandValidator : AbstractValidator<CreateLicenseAndCertificationSkillCommand>
{
    public CreateLicenseAndCertificationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.LicenseAndCertificationId).NotEmpty().WithMessage(LicenseAndCertificationSkillMessages.LicenseAndCertificationIdBosOlmamali);
        RuleFor(x => x.SkillId).NotEmpty().WithMessage(LicenseAndCertificationSkillMessages.SkillIdBosOlmamali);
        #endregion
    }
}