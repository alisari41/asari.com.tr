using FluentValidation;

namespace asari.com.tr.Application.Features.EducationSkills.Commands.Update;

public class UpdateEducationSkillCommandValidator : AbstractValidator<UpdateEducationSkillCommand>
{
    public UpdateEducationSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Eğitim Yetenek Id'si boş bırakmayınız");
        RuleFor(x => x.EducationId).NotEmpty().WithMessage("Eğitim Id'si boş bırakmayınız");
        RuleFor(x => x.SkillId).NotEmpty().WithMessage("Yetenek Id'si boş bırakmayınız");
        #endregion
    }
}