using asari.com.tr.Application.Features.ExperienceSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Delete;

public class DeleteExperienceSkillCommandValidator : AbstractValidator<DeleteExperienceSkillCommand>
{
    public DeleteExperienceSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(ExperienceSkillMessages.IdBosOlmamali);
        #endregion        
    }
}
