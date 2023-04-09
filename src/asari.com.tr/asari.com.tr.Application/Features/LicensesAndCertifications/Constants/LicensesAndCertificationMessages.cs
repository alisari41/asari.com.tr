namespace asari.com.tr.Application.Features.LicensesAndCertifications.Constants;

public static class LicensesAndCertificationMessages
{
    #region İş Kuralı - Business Role
    public const string LisansVeSertifikaMevcutDegil = "Lisans ve Sertifika mevcut değildir.";
    public const string LisansVeSertifikaMevcut = "Lisans ve Sertifika sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
    #region Zorunlu Alanlar
    public const string IdBosOlmamali = "'Id'si boş olmamalıdır.";
    public const string NameBosOlmamali = "'Lisans ve Sertifika Ad'ı boş olmamalıdır.";
    public const string IssuingOrganizationBosOlmamali = "'Organizasyon' boş olmamalıdır.";
    #endregion
    #region Max Karakter Uzunluğu
    public const string NameMaxKarakter = "'Lisans ve Sertifika Adı' en fazla 350 karakter olmalıdır.";
    public const string IssuingOrganizationMaxKarakter = "'Organizasyon' en fazla 250 karakter olmalıdır.";
    #endregion
    #endregion

}