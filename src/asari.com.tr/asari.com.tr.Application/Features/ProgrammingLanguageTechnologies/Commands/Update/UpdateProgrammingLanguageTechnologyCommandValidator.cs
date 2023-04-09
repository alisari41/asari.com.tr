using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Update;

public class UpdateProgrammingLanguageTechnologyCommandValidator : AbstractValidator<UpdateProgrammingLanguageTechnologyCommand>
{
    public UpdateProgrammingLanguageTechnologyCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(ProgrammingLanguageTechnologyMessages.IdBosOlmamali);
        RuleFor(x => x.Name).NotEmpty().WithMessage(ProgrammingLanguageTechnologyMessages.NameBosOlmamali);
        RuleFor(x => x.ProgrammingLanguageId).NotEmpty().WithMessage(ProgrammingLanguageTechnologyMessages.ProgrammingLanguageIdBosOlmamali);
        RuleFor(x => x.Name).MaximumLength(150).WithMessage(ProgrammingLanguageTechnologyMessages.NameMaxKarakter);
    }
}