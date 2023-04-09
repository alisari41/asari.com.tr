using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Update;

public class UpdateLicenseAndCertificationSkillCommandValidator : AbstractValidator<UpdateLicenseAndCertificationSkillCommand>
{
    public UpdateLicenseAndCertificationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(LicenseAndCertificationSkillMessages.IdBosOlmamali);
        RuleFor(x => x.LicenseAndCertificationId).NotEmpty().WithMessage(LicenseAndCertificationSkillMessages.LicenseAndCertificationIdBosOlmamali);
        RuleFor(x => x.SkillId).NotEmpty().WithMessage(LicenseAndCertificationSkillMessages.SkillIdBosOlmamali);
        #endregion        
    }
}