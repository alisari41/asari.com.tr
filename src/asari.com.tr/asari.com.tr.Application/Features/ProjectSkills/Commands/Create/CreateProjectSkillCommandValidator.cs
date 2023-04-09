using asari.com.tr.Application.Features.ProjectSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ProjectSkills.Commands.Create;

public class CreateProjectSkillCommandValidator : AbstractValidator<CreateProjectSkillCommand>
{
    public CreateProjectSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage(ProjectSkillMessages.ProjectIdBosOlmamali);
        RuleFor(x => x.SkillId).NotEmpty().WithMessage(ProjectSkillMessages.SkillIdBosOlmamali);
        #endregion
    }
}