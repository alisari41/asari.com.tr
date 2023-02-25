using FluentValidation;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.UpdateProgrammingLanguageTechnology;

public class UpdateProgrammingLanguageTechnologyCommandValidator : AbstractValidator<UpdateProgrammingLanguageTechnologyCommand>
{
    public UpdateProgrammingLanguageTechnologyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Programlama Dili Teknolojisi adını boş bırakmayınız");
        RuleFor(x => x.ProgrammingLanguageId).NotEmpty().WithMessage("Programlama Dili Id'sini boş bırakmayınız");
    }
}
