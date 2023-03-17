using FluentValidation;

namespace asari.com.tr.Application.Features.ProjectSkills.Commands.Create;

public class CreateProjectSkillCommandValidator : AbstractValidator<CreateProjectSkillCommand>
{
    public CreateProjectSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Proje Id'si boş bırakmayınız");
        RuleFor(x => x.SkillId).NotEmpty().WithMessage("Yetenek Id'si boş bırakmayınız");
        #endregion
    }
}