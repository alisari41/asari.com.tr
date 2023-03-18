using FluentValidation;

namespace asari.com.tr.Application.Features.ExperienceSkills.Commands.Delete;

public class DeleteExperienceSkillCommandValidator : AbstractValidator<DeleteExperienceSkillCommand>
{
    public DeleteExperienceSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Deneyim Yetenek Id'si boş bırakmayınız");
        #endregion        
    }
}
