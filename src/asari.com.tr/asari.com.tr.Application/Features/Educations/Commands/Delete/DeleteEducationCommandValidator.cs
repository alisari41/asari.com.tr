using FluentValidation;

namespace asari.com.tr.Application.Features.Educations.Commands.Delete;

public class DeleteEducationCommandValidator:AbstractValidator<DeleteEducationCommand>
{
    public DeleteEducationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Eğitim Id'si boş bırakmayınız");
        #endregion
    }
}