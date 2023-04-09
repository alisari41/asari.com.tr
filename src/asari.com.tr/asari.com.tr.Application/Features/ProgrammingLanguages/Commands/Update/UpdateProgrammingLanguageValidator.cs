using asari.com.tr.Application.Features.ProgrammingLanguages.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Update;

public class UpdateProgrammingLanguageValidator : AbstractValidator<UpdateProgrammingLanguageCommand>
{
    public UpdateProgrammingLanguageValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage(ProgrammingLanguageMessages.IdBosOlmamali);
        RuleFor(x => x.Name).NotEmpty().WithMessage(ProgrammingLanguageMessages.NameBosOlmamali); // Boş Geçilemez
        RuleFor(x => x.Name).MaximumLength(50).WithMessage(ProgrammingLanguageMessages.NameMaxKarakter);
    }
}