using FluentValidation;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Command.Update;

public class UpdateLicenseAndCertificationSkillCommandValidator : AbstractValidator<UpdateLicenseAndCertificationSkillCommand>
{
    public UpdateLicenseAndCertificationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Lisans ve Sertifika Yetenek Id'si boş bırakmayınız");
        RuleFor(x => x.LicenseAndCertificationId).NotEmpty().WithMessage("Lisans ve Sertifika Id'si boş bırakmayınız");
        RuleFor(x => x.SkillId).NotEmpty().WithMessage("Yetenek Id'si boş bırakmayınız");
        #endregion        
    }
}