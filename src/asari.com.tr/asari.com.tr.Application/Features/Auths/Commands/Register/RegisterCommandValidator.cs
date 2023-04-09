using asari.com.tr.Application.Features.Auths.Constants;
using FluentValidation;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.RegularExpressions;

namespace asari.com.tr.Application.Features.Auths.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.UserForRegisterDto.FirstName)
            .NotEmpty().WithMessage(AuthMessages.AdBosOlmamali)
            .Matches(@"^[A-Za-z-9ğüşöçıİĞÜŞÖÇ]*$").WithMessage(AuthMessages.AdYanlizcaHarfIcermeli)
            .Length(3, 30).WithMessage(AuthMessages.AdMinMaxKarakter);


        RuleFor(c => c.UserForRegisterDto.LastName)
            .NotEmpty().WithMessage(AuthMessages.SoyadBosOlmamali)
            .Matches(@"^[A-Za-z-9ğüşöçıİĞÜŞÖÇ]*$").WithMessage(AuthMessages.SoyadYanlizcaHarfIcermeli) // 9ğüşöçıİĞÜŞÖÇ dahil olmayan türkçe karakterleride dahil eder. Yoksa SARI daki I hata verir.
            .Length(2, 30).WithMessage(AuthMessages.SoyadMinMaxKarakter);

        RuleFor(c => c.UserForRegisterDto.Email)
            .NotEmpty().WithMessage(AuthMessages.EPostaBosOlmamali)
            .EmailAddress().WithMessage(AuthMessages.GecerliEPosta);

        RuleFor(c => c.UserForRegisterDto.Password)
            .NotEmpty()
            .MinimumLength(8).WithMessage(AuthMessages.SifreMinKarakter)
            .Matches("[A-Z-9İĞÜŞÖÇ]").WithMessage(AuthMessages.SifreMinBuyukKarakter)
            .Matches("[a-z-9ğüşöçı]").WithMessage(AuthMessages.SifreMinKucukKarakter)
            .Matches(@"\d").WithMessage(AuthMessages.SifreMinRakam)
            .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage(AuthMessages.SifreMinOzelKarakter)
            .Matches("^[^£# “”]*$").WithMessage(AuthMessages.SifreIcermemeli);
    }
}