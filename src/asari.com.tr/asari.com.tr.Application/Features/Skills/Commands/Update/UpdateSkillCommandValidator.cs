using asari.com.tr.Application.Features.Skills.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.Skills.Commands.Update;

public class UpdateSkillCommandValidator : AbstractValidator<UpdateSkillCommand>
{
    public UpdateSkillCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(SkillMessages.IdBosOlmamali);
        RuleFor(x => x.Name).NotEmpty().WithMessage(SkillMessages.NameBosOlmamali);
        RuleFor(x => x.Degree).NotEmpty().WithMessage(SkillMessages.DegreeBosOlmamali);
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Name).MaximumLength(250).WithMessage(SkillMessages.NameMaxKarakter);
        RuleFor(x => x.Degree).Must(HaveOneDecimalPlace).WithMessage(SkillMessages.DegreeVirguldenSonraMaxKarakter);
        RuleFor(x => x.Degree).LessThanOrEqualTo(10).WithMessage(SkillMessages.DegreeMaxKarakter);
        #endregion
    }

    private bool HaveOneDecimalPlace(double? value)
    {
        if (value == null)
        {
            return false;
        }

        string stringValue = value.ToString();

        if (!stringValue.Contains(","))
        {
            return true; // Virgül yoksa, kabul et
        }

        string[] parts = stringValue.Split(',');

        if (parts[1].Length != 1)
        {
            return false; // Virgülden sonra sadece 1 rakam içermiyor, hatalı durum
        }

        return true; // Geçerli durum
    }
}
