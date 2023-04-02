using FluentValidation;
using System.Text.RegularExpressions;

namespace asari.com.tr.Application.Features.Auths.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.UserForLoginDto.Email)
            .NotEmpty().WithMessage("'E-posta' boş olmamalıdır.")
            .EmailAddress().WithMessage("Geçerli bir e-posta değeri giriniz!");

        RuleFor(c => c.UserForLoginDto.Password)
            .NotEmpty()
            .MinimumLength(8).WithMessage("'Şifre' en az 8 karakter olmalıdır.")
            .Matches("[A-Z]").WithMessage("'Şifre' bir veya daha fazla büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("'Şifre' bir veya daha fazla küçük harf içermelidir.")
            .Matches(@"\d").WithMessage("'Şifre' bir veya daha fazla rakam içermelidir.")
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("'Şifre' bir veya daha fazla özel karakter içermelidir.")
            .Matches("^[^£# “”]*$").WithMessage("'Şifre' £ # “” karakterleri veya boşluk içermemelidir.");
    }
}