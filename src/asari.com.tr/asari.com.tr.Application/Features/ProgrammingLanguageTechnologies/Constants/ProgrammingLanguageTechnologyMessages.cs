namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Constants;

public static class ProgrammingLanguageTechnologyMessages
{
    #region İş Kuralı - Business Role
    public const string ProgramlamaDiliTeknolojisiMevcutDegil = "Programlama dili Teknolojisi mevcut değildir.";
    public const string ProgramlamaDiliTeknolojisiMevcut = "Programlama dili Teknolojisi sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
    #region Zorunlu Alanlar
    public const string IdBosOlmamali = "'Id'si boş olmamalıdır.";
    public const string NameBosOlmamali = "'Programlama Dili Teknoloji'si boş olmamalıdır.";
    public const string ProgrammingLanguageIdBosOlmamali = "'Programlama Dili Id'si boş olmamalıdır.";
    #endregion
    #region Max Karakter Uzunluğu
    public const string NameMaxKarakter = "'Programlama Dili Teknoloji Adı' en fazla 150 karakter olmalıdır.";
    #endregion
    #endregion
}