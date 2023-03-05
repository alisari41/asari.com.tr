using FluentValidation;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Update;

public class UpdateProgrammingLanguageValidator : AbstractValidator<UpdateProgrammingLanguageCommand>
{
    public UpdateProgrammingLanguageValidator()
    {
        RuleFor(c => c.Id).NotEmpty().WithMessage("Programlama Dili Id'si Boş olamaz");
        RuleFor(c => c.Name).NotEmpty().WithMessage("Programlama dili adı Boş Geçilemez");
        RuleFor(c => c.Name).MaximumLength(50).WithMessage("Programlama dili adı en fazla 50 karakterden oluşabilir.");
    }
}