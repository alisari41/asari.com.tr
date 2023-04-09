using asari.com.tr.Application.Features.Auths.Constants;
using FluentValidation;
using System.Text.RegularExpressions;

namespace asari.com.tr.Application.Features.Auths.Commands.Login;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.UserForLoginDto.Email)
            .NotEmpty().WithMessage(AuthMessages.EPostaBosOlmamali)
            .EmailAddress().WithMessage(AuthMessages.GecerliEPosta);

        RuleFor(c => c.UserForLoginDto.Password)
            .NotEmpty()
            .MinimumLength(8).WithMessage(AuthMessages.SifreMinKarakter)
            .Matches("[A-Z-9İĞÜŞÖÇ]").WithMessage(AuthMessages.SifreMinBuyukKarakter)
            .Matches("[a-z-9ğüşöçı]").WithMessage(AuthMessages.SifreMinKucukKarakter)
            .Matches(@"\d").WithMessage(AuthMessages.SifreMinRakam)
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage(AuthMessages.SifreMinOzelKarakter)
            .Matches("^[^£# “”]*$").WithMessage(AuthMessages.SifreIcermemeli);
    }
}