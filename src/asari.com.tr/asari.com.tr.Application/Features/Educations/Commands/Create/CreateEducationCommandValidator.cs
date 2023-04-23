using asari.com.tr.Application.Features.Educations.Constants;
using asari.com.tr.Application.Services.Repositories;
using FluentValidation;

namespace asari.com.tr.Application.Features.Educations.Commands.Create;

public class CreateEducationCommandValidator : AbstractValidator<CreateEducationCommand>
{
    public CreateEducationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Name).NotEmpty().WithMessage(EducationMessages.NameBosOlmamali);
        RuleFor(x => x.Degree).NotNull().NotEmpty().WithMessage(EducationMessages.DegreeBosOlmamali);
        RuleFor(x => x.FieldOfStudy).NotEmpty().WithMessage(EducationMessages.FieldOfStudyBosOlmamali);
        RuleFor(x => x.StartDate).NotEmpty().WithMessage(EducationMessages.StartDateBosOlmamali);
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Name).MaximumLength(250).WithMessage(EducationMessages.NameMaxKarakter);
        RuleFor(x => x.FieldOfStudy).MaximumLength(100).WithMessage(EducationMessages.FieldOfStudyMaxKarakter);
        RuleFor(x => x.Grade).MaximumLength(250).WithMessage(EducationMessages.GradeMaxKarakter);
        RuleFor(x => x.ActivityAndCommunity).MaximumLength(500).WithMessage(EducationMessages.ActivityAndCommunityMaxKarakter);
        RuleFor(x => x.Description).MaximumLength(1000).WithMessage(EducationMessages.DescriptionMaxKarakter);
        #endregion                
    }
}