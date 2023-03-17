using FluentValidation;

namespace asari.com.tr.Application.Features.ProjectSkills.Commands.Update;

public class UpdateProjectSkillCommandValidator : AbstractValidator<UpdateProjectSkillCommand>
{
    public UpdateProjectSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Proje Yetenek Id'si boş bırakmayınız");
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Proje Id'si boş bırakmayınız");
        RuleFor(x => x.SkillId).NotEmpty().WithMessage("Yetenek Id'si boş bırakmayınız");
        #endregion
    }
}
