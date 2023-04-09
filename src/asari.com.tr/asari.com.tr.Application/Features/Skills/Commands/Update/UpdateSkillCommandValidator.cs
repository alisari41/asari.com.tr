using asari.com.tr.Application.Features.Skills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Skills.Commands.Update;

public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(SkillMessages.IdBosOlmamali);
        RuleFor(x => x.Name).NotEmpty().WithMessage(SkillMessages.NameBosOlmamali);
        RuleFor(x => x.Degree).NotEmpty().WithMessage(SkillMessages.DegreeBosOlmamali);
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Name).MaximumLength(250).WithMessage(SkillMessages.NameMaxKarakter);
        #endregion
    }
}
