using asari.com.tr.Application.Features.EducationSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.EducationSkills.Commands.Delete;

public class DeleteEducationSkillCommandValidator : AbstractValidator<DeleteEducationSkillCommand>
{
    public DeleteEducationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(EducationSkillMessages.EducationSkillIdBosOlmamali);
        #endregion 
    }
}