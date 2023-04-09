using asari.com.tr.Application.Features.EducationSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.EducationSkills.Commands.Create;

public class CreateEducationSkillCommandValidator : AbstractValidator<CreateEducationSkillCommand>
{
    public CreateEducationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.EducationId).NotEmpty().WithMessage(EducationSkillMessages.EducationIdBosOlmamali);
        RuleFor(x => x.SkillId).NotEmpty().WithMessage(EducationSkillMessages.SkillIdBosOlmamali);
        #endregion
    }
}