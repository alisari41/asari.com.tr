using asari.com.tr.Application.Features.Projects.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Projects.Commands.Update;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(ProjectMessages.IdBosOlmamali);
        RuleFor(x => x.Title).NotEmpty().WithMessage(ProjectMessages.TitleBosOlmamali);
        RuleFor(x => x.Description).NotEmpty().WithMessage(ProjectMessages.DescriptionBosOlmamali);
        RuleFor(x => x.Content).NotEmpty().WithMessage(ProjectMessages.ContentBosOlmamali);
        RuleFor(x => x.CreateDate).NotEmpty().WithMessage(ProjectMessages.CreateDateBosOlmamali);
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Title).MaximumLength(250).WithMessage(ProjectMessages.TitleMaxKarakter);
        #endregion
    }
}