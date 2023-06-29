namespace asari.com.tr.Application.Features.Skills.Constants;

public static class SkillMessages
{
    #region İş Kuralı - Business Role
    public const string YetenekMevcutDegil = "Yetenek mevcut değildir.";
    public const string YetenekMevcut = "Yetenek sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Id'si boş olmamalıdır.";
        public const string NameBosOlmamali = "'Yetenek Adı' boş olmamalıdır.";
        public const string DegreeBosOlmamali = "'Derece' boş olmamalıdır.";
        #endregion
        #region Max Karakter Uzunluğu
        public const string NameMaxKarakter = "'Yetenek Adı' en fazla 250 karakter olmalıdır.";
        public const string DegreeVirguldenSonraMaxKarakter = "'Yetenek Derecesi' virgülden sonra sadece 1 rakam içermelidir.";
        public const string DegreeMaxKarakter = "'Yetenek Derecesi' en fazla 10 değerini olmalıdır.";
        #endregion
    #endregion
}