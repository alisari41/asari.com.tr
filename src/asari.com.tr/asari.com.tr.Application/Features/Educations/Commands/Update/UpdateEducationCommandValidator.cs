using asari.com.tr.Application.Features.Educations.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Educations.Commands.Update;

public class UpdateEducationCommandValidator : AbstractValidator<UpdateEducationCommand>
{
    public UpdateEducationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(EducationMessages.IdBosOlmamali);
        RuleFor(x => x.Name).NotEmpty().WithMessage(EducationMessages.NameBosOlmamali);
        RuleFor(x => x.Degree).NotNull().NotEmpty().WithMessage(EducationMessages.DegreeBosOlmamali);
        RuleFor(x => x.FieldOfStudy).NotEmpty().WithMessage(EducationMessages.FieldOfStudyBosOlmamali);
        RuleFor(x => x.StartDate).NotEmpty().WithMessage(EducationMessages.StartDateBosOlmamali);
        #endregion

        #region Minimum Karakter Uzunluğu
        RuleFor(x => x.Name).MinimumLength(3).WithMessage(EducationMessages.NameMinKarakter);
        RuleFor(x => x.FieldOfStudy).MinimumLength(3).WithMessage(EducationMessages.FieldOfStudyMinKarakter);
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
