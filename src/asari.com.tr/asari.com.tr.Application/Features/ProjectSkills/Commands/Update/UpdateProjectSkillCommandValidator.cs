using asari.com.tr.Application.Features.ProjectSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ProjectSkills.Commands.Update;

public class UpdateProjectSkillCommandValidator : AbstractValidator<UpdateProjectSkillCommand>
{
    public UpdateProjectSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(ProjectSkillMessages.IdBosOlmamali);
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage(ProjectSkillMessages.ProjectIdBosOlmamali);
        RuleFor(x => x.SkillId).NotEmpty().WithMessage(ProjectSkillMessages.SkillIdBosOlmamali);
        #endregion
    }
}
