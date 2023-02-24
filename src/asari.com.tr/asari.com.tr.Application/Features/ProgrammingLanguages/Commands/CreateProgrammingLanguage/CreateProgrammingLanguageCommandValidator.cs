﻿using asari.com.tr.Application.Features.ProgrammingLanguages.Dtos;
using FluentValidation;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;

public class CreateProgrammingLanguageCommandValidator : AbstractValidator<CreateProgrammingLanguageCommand>
{
    // FluentValidation ile Format Doğrulama işlemleri
    // Ekeleme işlemleri için ayrı güncelleme işlemleri vs. ler için ayrı doğrulama işlemleri olabileceği için "CreateBrandCommand" ile ekleme işlemleri için yapıldı

    public CreateProgrammingLanguageCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name Boş Geçilemez"); // Boş Geçilemez
    }
}
