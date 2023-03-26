using FluentValidation;

namespace asari.com.tr.Application.Features.EducationSkills.Commands.Create;

public class CreateEducationSkillCommandValidator : AbstractValidator<CreateEducationSkillCommand>
{
    public CreateEducationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.EducationId).NotEmpty().WithMessage("Eğitim Id'si boş bırakmayınız");
        RuleFor(x => x.SkillId).NotEmpty().WithMessage("Yetenek Id'si boş bırakmayınız");
        #endregion
    }
}