using asari.com.tr.Application.Features.EducationSkills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.EducationSkills.Commands.Update;

public class UpdateEducationSkillCommandValidator : AbstractValidator<UpdateEducationSkillCommand>
{
    public UpdateEducationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(EducationSkillMessages.EducationSkillIdBosOlmamali);
        RuleFor(x => x.EducationId).NotEmpty().WithMessage(EducationSkillMessages.EducationIdBosOlmamali);
        RuleFor(x => x.SkillId).NotEmpty().WithMessage(EducationSkillMessages.SkillIdBosOlmamali);
        #endregion
    }
}