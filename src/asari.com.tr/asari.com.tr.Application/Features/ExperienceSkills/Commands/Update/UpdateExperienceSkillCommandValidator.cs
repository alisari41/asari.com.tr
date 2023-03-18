using FluentValidation;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Update;

public class UpdateExperienceSkillCommandValidator : AbstractValidator<UpdateExperienceSkillCommand>
{
    public UpdateExperienceSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Deneyim Yetenek Id'si boş bırakmayınız");
        RuleFor(x => x.ExperienceId).NotEmpty().WithMessage("Deneyim Id'si boş bırakmayınız");
        RuleFor(x => x.SkillId).NotEmpty().WithMessage("Yetenek Id'si boş bırakmayınız");
        #endregion 
    }
}