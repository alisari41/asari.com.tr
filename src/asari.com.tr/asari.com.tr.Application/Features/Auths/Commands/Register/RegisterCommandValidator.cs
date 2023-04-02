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
            .NotEmpty()
            .MinimumLength(8).WithMessage("'Şifre' en az 8 karakter olmalıdır.")
            .Matches("[A-Z]").WithMessage("'Şifre' bir veya daha fazla büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("'Şifre' bir veya daha fazla küçük harf içermelidir.")
            .Matches(@"\d").WithMessage("'Şifre' bir veya daha fazla rakam içermelidir.")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'Şifre' bir veya daha fazla özel karakter içermelidir.")
            .Matches("^[^£# “”]*$").WithMessage("'Şifre' £ # “” karakterleri veya boşluk içermemelidir.");
    }
}