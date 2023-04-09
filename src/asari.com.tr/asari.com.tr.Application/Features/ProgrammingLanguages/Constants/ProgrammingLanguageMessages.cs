namespace asari.com.tr.Application.Features.ProgrammingLanguages.Constants;

public static class ProgrammingLanguageMessages
{
    #region İş Kuralı - Business Role
    public const string ProgramlamaDiliMevcutDegil = "Programlama dili mevcut değildir.";
    public const string ProgramlamaDiliMevcut = "Programlama dili sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Id'si boş olmamalıdır.";
        public const string NameBosOlmamali = "'Programlama Dil'i boş olmamalıdır.";
        #endregion
        #region Max Karakter Uzunluğu
        public const string NameMaxKarakter = "'Programlama Dili' en fazla 50 karakter olmalıdır.";
        #endregion
    #endregion
}