using FluentValidation;

namespace asari.com.tr.Application.Features.Experiences.Commands.Create;

public class CreateExperienceCommandValidator : AbstractValidator<CreateExperienceCommand>
{
    public CreateExperienceCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Title).NotEmpty().WithMessage("Deneyim Başlığı boş bırakmayınız");
        RuleFor(x => x.EmploymentType).NotEmpty().WithMessage("İstihdam türü'nü boş bırakmayınız");
        RuleFor(x => x.CompanyName).NotEmpty().WithMessage("Şirket Adını boş bırakmayınız");
        RuleFor(x => x.Location).NotEmpty().WithMessage("Konum'u boş bırakmayınız");
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("Başlama Tarihini boş bırakmayınız");
        RuleFor(x => x.Industry).NotEmpty().WithMessage("Sektör alanını boş bırakmayınız");
        RuleFor(x => x.ProfileHeadline).NotEmpty().WithMessage("Profil Başlığı alanını boş bırakmayınız");
        #endregion

        #region Maximum Karakter Uzunluğu
        RuleFor(x => x.Title).MaximumLength(250).WithMessage("Deneyim Başlığı 250 karakterden uzun olamaz.");
        RuleFor(x => x.EmploymentType).MaximumLength(50).WithMessage("İstihdam Türü 50 karakterden uzun olamaz.");
        RuleFor(x => x.CompanyName).MaximumLength(250).WithMessage("Şirket Adı 250 karakterden uzun olamaz.");
        RuleFor(x => x.Location).MaximumLength(250).WithMessage("Konum 250 karakterden uzun olamaz.");
        RuleFor(x => x.Industry).MaximumLength(250).WithMessage("Sektör 250 karakterden uzun olamaz.");
        #endregion        
    }
}