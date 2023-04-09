namespace asari.com.tr.Application.Features.Experiences.Constants;

public static class ExperienceMessages
{
    #region İş Kuralı - Business Role
    public const string DeneyimMevcutDegil = "Deneyim mevcut değildir.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Id'si boş olmamalıdır.";
        public const string TitleBosOlmamali = "'Başlık' boş olmamalıdır.";
        public const string EmploymentTypeBosOlmamali = "'İstihdam türü' boş olmamalıdır.";
        public const string CompanyNameBosOlmamali = "'Şirket Adı' boş olmamalıdır.";
        public const string LocationBosOlmamali = "'Konum' boş olmamalıdır.";
        public const string StartDateBosOlmamali = "'Başlama Tarihi' boş olmamalıdır.";
        public const string IndustryBosOlmamali = "'Sektör alanı' boş olmamalıdır.";
        public const string ProfileHeadlineBosOlmamali = "'Profil Başlığı' boş olmamalıdır.";

        #endregion
        #region Max Karakter Uzunluğu
        public const string TitleMaxKarakter = "'Başlık' en fazla 250 karakter olmalıdır.";
        public const string EmploymentTypeMaxKarakter = "'İstihdam türü' en fazla 50 karakter olmalıdır.";
        public const string CompanyNameMaxKarakter = "'Şirket Adı' en fazla 250 karakter olmalıdır.";
        public const string LocationMaxKarakter = "'Konum' en fazla 250 karakter olmalıdır.";
        public const string IndustryMaxKarakter = "'Sektör alanı' en fazla 250 karakter olmalıdır.";
        #endregion
    #endregion
}