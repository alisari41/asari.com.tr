using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Create;

public class CreateProgrammingLanguageTechnologyCommandValidator : AbstractValidator<CreateProgrammingLanguageTechnologyCommand>
{
    public CreateProgrammingLanguageTechnologyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(ProgrammingLanguageTechnologyMessages.NameBosOlmamali);
        RuleFor(x => x.ProgrammingLanguageId).NotEmpty().WithMessage(ProgrammingLanguageTechnologyMessages.ProgrammingLanguageIdBosOlmamali);
        RuleFor(x => x.Name).MaximumLength(150).WithMessage(ProgrammingLanguageTechnologyMessages.NameMaxKarakter);
    }
}