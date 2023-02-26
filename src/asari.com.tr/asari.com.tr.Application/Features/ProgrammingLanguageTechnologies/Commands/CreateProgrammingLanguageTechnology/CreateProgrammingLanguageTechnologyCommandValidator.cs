﻿using FluentValidation;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.CreateProgrammingLanguageTechnology;

public class CreateProgrammingLanguageTechnologyCommandValidator : AbstractValidator<CreateProgrammingLanguageTechnologyCommand>
{
    public CreateProgrammingLanguageTechnologyCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Programlama Dili Teknolojisi adını boş bırakmayınız");
        RuleFor(x => x.ProgrammingLanguageId).NotEmpty().WithMessage("Programlama Dili Id'sini boş bırakmayınız");
    }
}