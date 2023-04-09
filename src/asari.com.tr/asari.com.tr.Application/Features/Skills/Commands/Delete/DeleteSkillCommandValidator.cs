using asari.com.tr.Application.Features.Skills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Skills.Commands.Delete;

public class DeleteSkillCommandValidator : AbstractValidator<DeleteSkillCommand>
{
    public DeleteSkillCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(SkillMessages.IdBosOlmamali);
    }
}