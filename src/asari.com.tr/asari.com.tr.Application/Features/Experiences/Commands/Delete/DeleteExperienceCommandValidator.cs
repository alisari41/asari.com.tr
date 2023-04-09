using asari.com.tr.Application.Features.Experiences.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Experiences.Commands.Delete;

public class DeleteExperienceCommandValidator : AbstractValidator<DeleteExperienceCommand>
{
    public DeleteExperienceCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(ExperienceMessages.IdBosOlmamali);
    }
}