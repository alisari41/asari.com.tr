using FluentValidation;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Create;

public class CreateExperienceSkillCommandValidator:AbstractValidator<CreateExperienceSkillCommand>
{
    public CreateExperienceSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.ExperienceId).NotEmpty().WithMessage("Deneyim Id'si boş bırakmayınız");
        RuleFor(x => x.SkillId).NotEmpty().WithMessage("Yetenek Id'si boş bırakmayınız");
        #endregion        
    }
}
