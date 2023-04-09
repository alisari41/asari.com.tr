using asari.com.tr.Application.Features.Technologies.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Technologies.Commands.Delete;

public class DeleteTechnologyCommandValidator : AbstractValidator<DeleteTechnologyCommand>
{
    public DeleteTechnologyCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(TechnologyMessages.IdBosOlmamali);
    }
}