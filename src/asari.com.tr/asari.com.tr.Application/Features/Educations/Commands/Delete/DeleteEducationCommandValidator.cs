using asari.com.tr.Application.Features.Educations.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Educations.Commands.Delete;

public class DeleteEducationCommandValidator : AbstractValidator<DeleteEducationCommand>
{
    public DeleteEducationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage(EducationMessages.IdBosOlmamali);
        #endregion
    }
}