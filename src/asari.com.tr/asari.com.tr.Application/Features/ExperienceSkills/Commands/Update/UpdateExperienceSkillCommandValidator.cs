using asari.com.tr.Application.Features.ExperienceSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Update;

public class UpdateExperienceSkillCommandValidator : AbstractValidator<UpdateExperienceSkillCommand>
{
    public UpdateExperienceSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(ExperienceSkillMessages.IdBosOlmamali);
        RuleFor(x => x.ExperienceId).NotEmpty().WithMessage(ExperienceSkillMessages.ExperienceIdBosOlmamali);
        RuleFor(x => x.SkillId).NotEmpty().WithMessage(ExperienceSkillMessages.SkillIdBosOlmamali);
        #endregion 
    }
}