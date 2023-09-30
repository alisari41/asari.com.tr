using asari.com.tr.Application.Features.Experiences.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Experiences.Commands.Update;

public class UpdateExperienceCommandValidator : AbstractValidator<UpdateExperienceCommand>
{
    public UpdateExperienceCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(ExperienceMessages.IdBosOlmamali);
        RuleFor(x => x.Title).NotEmpty().WithMessage(ExperienceMessages.TitleBosOlmamali);
        RuleFor(x => x.EmploymentType).NotEmpty().WithMessage(ExperienceMessages.EmploymentTypeBosOlmamali);
        RuleFor(x => x.CompanyName).NotEmpty().WithMessage(ExperienceMessages.CompanyNameBosOlmamali);
        RuleFor(x => x.Location).NotEmpty().WithMessage(ExperienceMessages.LocationBosOlmamali);
        RuleFor(x => x.StartDate).NotEmpty().WithMessage(ExperienceMessages.StartDateBosOlmamali);
        RuleFor(x => x.Industry).NotEmpty().WithMessage(ExperienceMessages.IndustryBosOlmamali);
        RuleFor(x => x.ProfileHeadline).NotEmpty().WithMessage(ExperienceMessages.ProfileHeadlineBosOlmamali);
        #endregion

        #region Minimum Karakter Uzunluğu
        RuleFor(x => x.Title).MinimumLength(3).WithMessage(ExperienceMessages.TitleMinKarakter);
        RuleFor(x => x.EmploymentType).MinimumLength(3).WithMessage(ExperienceMessages.EmploymentTypeMinKarakter);
        RuleFor(x => x.CompanyName).MinimumLength(3).WithMessage(ExperienceMessages.CompanyNameMinKarakter);
        RuleFor(x => x.Location).MinimumLength(3).WithMessage(ExperienceMessages.LocationMinKarakter);
        RuleFor(x => x.Industry).MinimumLength(3).WithMessage(ExperienceMessages.IndustryMinKarakter);
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Title).MaximumLength(250).WithMessage(ExperienceMessages.TitleMaxKarakter);
        RuleFor(x => x.EmploymentType).MaximumLength(50).WithMessage(ExperienceMessages.EmploymentTypeMaxKarakter);
        RuleFor(x => x.CompanyName).MaximumLength(250).WithMessage(ExperienceMessages.CompanyNameMaxKarakter);
        RuleFor(x => x.Location).MaximumLength(250).WithMessage(ExperienceMessages.LocationMaxKarakter);
        RuleFor(x => x.Industry).MaximumLength(250).WithMessage(ExperienceMessages.IndustryMaxKarakter);
        #endregion
    }
}