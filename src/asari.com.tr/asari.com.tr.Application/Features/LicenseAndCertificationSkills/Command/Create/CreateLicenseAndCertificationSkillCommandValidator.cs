using FluentValidation;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Create;

public class CreateLicenseAndCertificationSkillCommandValidator : AbstractValidator<CreateLicenseAndCertificationSkillCommand>
{
    public CreateLicenseAndCertificationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.LicenseAndCertificationId).NotEmpty().WithMessage("Lisans ve Sertifika Id'si boş bırakmayınız");
        RuleFor(x => x.SkillId).NotEmpty().WithMessage("Yetenek Id'si boş bırakmayınız");
        #endregion
    }
}