using asari.com.tr.Application.Features.ExperienceSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Create;

public class CreateExperienceSkillCommandValidator : AbstractValidator<CreateExperienceSkillCommand>
{
    public CreateExperienceSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.ExperienceId).NotEmpty().WithMessage(ExperienceSkillMessages.ExperienceIdBosOlmamali);
        RuleFor(x => x.SkillId).NotEmpty().WithMessage(ExperienceSkillMessages.SkillIdBosOlmamali);
        #endregion        
    }
}
