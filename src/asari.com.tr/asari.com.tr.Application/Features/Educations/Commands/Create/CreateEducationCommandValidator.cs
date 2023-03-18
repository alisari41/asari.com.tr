using asari.com.tr.Application.Services.Repositories;
using FluentValidation;

namespace asari.com.tr.Application.Features.Educations.Commands.Create;

public class CreateEducationCommandValidator : AbstractValidator<CreateEducationCommand>
{
    public CreateEducationCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Name).NotEmpty().WithMessage("Eğitim Başlığını boş bırakmayınız");
        RuleFor(x => x.Degree).NotEmpty().WithMessage("Eğitim Derecesini boş bırakmayınız");
        RuleFor(x => x.FieldOfStudy).NotEmpty().WithMessage("Bölüm boş bırakmayınız");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Başlangıç Tarihi boş bırakmayınız");
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Name).MaximumLength(250).WithMessage("Eğitim Başlığı 250 karakterden uzun olamaz.");
        RuleFor(x => x.FieldOfStudy).MaximumLength(100).WithMessage("Bölüm 100 karakterden uzun olamaz.");
        RuleFor(x => x.Grade).MaximumLength(250).WithMessage("Not alanı 250 karakterden uzun olamaz.");
        RuleFor(x => x.ActivityAndCommunity).MaximumLength(500).WithMessage("Faaliyet ve topluluklar alanı 500 karakterden uzun olamaz.");
        RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Açıklama alanı 1000 karakterden uzun olamaz.");
        #endregion                
    }
}