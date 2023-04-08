using FluentValidation;

namespace asari.com.tr.Application.Features.Skills.Commands.Update;

public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Yetenek Id'si boş bırakmayınız");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Yetenek Adını boş bırakmayınız");
        RuleFor(x => x.Degree).NotEmpty().WithMessage("Yetenek Derecesi boş bırakmayınız");
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Name).MaximumLength(250).WithMessage("Yetenek Adı 250 karakterden uzun olamaz.");
        #endregion
    }
}
