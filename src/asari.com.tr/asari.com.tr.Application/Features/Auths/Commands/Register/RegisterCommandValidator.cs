using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace asari.com.tr.Application.Features.Auths.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserForRegisterDto.FirstName)
            .NotEmpty().WithMessage("'Ad' boş olmamalıdır.")
            .Matches(@"^[A-Za-z-9ğüşöçıİĞÜŞÖÇ]*$").WithMessage("'Ad' yalnızca harf içermelidir.")
            .Length(3, 30).WithMessage("'Ad' 3 ve 30 arasında karakter uzunluğunda olmalı. Toplam {TotalLength} adet karakter girdiniz.");


        RuleFor(c => c.UserForRegisterDto.LastName)
            .NotEmpty().WithMessage("'Soyad' boş olmamalıdır.")
            .Matches(@"^[A-Za-z-9ğüşöçıİĞÜŞÖÇ]*$").WithMessage("'Soyad' yalnızca harf içermelidir.") // 9ğüşöçıİĞÜŞÖÇ dahil olmayan türkçe karakterleride dahil eder. Yoksa SARI daki I hata verir.
            .Length(3, 30).WithMessage("'Soyad' 3 ve 30 arasında karakter uzunluğunda olmalı. Toplam {TotalLength} adet karakter girdiniz.");

        RuleFor(c => c.UserForRegisterDto.Email)
            .NotEmpty().WithMessage("'E-posta' boş olmamalıdır.")
            .EmailAddress().WithMessage("Geçerli bir e-posta değeri giriniz!");

        RuleFor(c => c.UserForRegisterDto.Password)
            .NotEmpty().WithMessage("'Parola' alanı boş geçilemez!")
            .Must(IsPasswordValid).WithMessage("Parolanız en az 8 karakter, en az bir harf ve bir sayı içermelidir!");
    }

    private bool IsPasswordValid(string arg)
    {
        // Parola kontrolü - Parolanız en az 8 karakter, en az bir harf ve bir sayı içermelidir!
        Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
        return regex.IsMatch(arg);
    }
}