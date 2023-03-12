using FluentValidation;

namespace asari.com.tr.Application.Features.TechnologyProjects.Commands.Update;

public class UpdateTechnologyProjectCommandValidator : AbstractValidator<UpdateTechnologyProjectCommand>
{
    public UpdateTechnologyProjectCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Teknoloji Proje Id'si Boş olamaz");
        RuleFor(x => x.TechnologyId).NotEmpty().WithMessage("Teknoloji Id'si Boş olamaz");
        RuleFor(x => x.ProjectId).NotEmpty().WithMessage("Proje Id'si boş bırakmayınız");
        #endregion
    }
}
