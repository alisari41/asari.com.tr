using asari.com.tr.Application.Features.TechnologyProjects.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.TechnologyProjects.Commands.Update;

public class UpdateTechnologyProjectCommandValidator : AbstractValidator<UpdateTechnologyProjectCommand>
{
    public UpdateTechnologyProjectCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(TechnologyProjectMessages.IdBosOlmamali);
        RuleFor(x => x.TechnologyId).NotEmpty().WithMessage(TechnologyProjectMessages.TechnologyIdBosOlmamali);
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage(TechnologyProjectMessages.ProjectIdBosOlmamali);
        #endregion
    }
}
