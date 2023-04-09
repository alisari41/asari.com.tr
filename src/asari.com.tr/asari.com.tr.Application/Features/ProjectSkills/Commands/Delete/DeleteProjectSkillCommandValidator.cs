using asari.com.tr.Application.Features.ProjectSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ProjectSkills.Commands.Delete;

public class DeleteProjectSkillCommandValidator : AbstractValidator<DeleteProjectSkillCommand>
{
    public DeleteProjectSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(ProjectSkillMessages.IdBosOlmamali);
        #endregion
    }
}