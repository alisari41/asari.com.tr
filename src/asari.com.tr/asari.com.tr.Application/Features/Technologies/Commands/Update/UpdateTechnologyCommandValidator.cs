using asari.com.tr.Application.Features.Technologies.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Technologies.Commands.Update;

public class UpdateTechnologyCommandValidator : AbstractValidator<UpdateTechnologyCommand>
{
    public UpdateTechnologyCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(TechnologyMessages.IdBosOlmamali);
        RuleFor(x => x.Title).NotEmpty().WithMessage(TechnologyMessages.TitleBosOlmamali);
        RuleFor(x => x.Description).NotEmpty().WithMessage(TechnologyMessages.DescriptionBosOlmamali);
        RuleFor(x => x.Content).NotEmpty().WithMessage(TechnologyMessages.ContentBosOlmamali);
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Title).MaximumLength(250).WithMessage(TechnologyMessages.TitleMaxKarakter);
        #endregion
    }
}