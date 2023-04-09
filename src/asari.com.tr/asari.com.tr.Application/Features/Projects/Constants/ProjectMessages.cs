namespace asari.com.tr.Application.Features.Projects.Constants;

public static class ProjectMessages
{
    #region İş Kuralı - Business Role
    public const string ProjeMevcutDegil = "Proje mevcut değildir.";
    public const string ProjeMevcut = "Proje sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Id'si boş olmamalıdır.";
        public const string TitleBosOlmamali = "'Proje Adı' boş olmamalıdır.";
        public const string DescriptionBosOlmamali = "'Açıklama' boş olmamalıdır.";
        public const string ContentBosOlmamali = "'İçerik' boş olmamalıdır.";
        public const string CreateDateBosOlmamali = "'Tarih' boş olmamalıdır.";
        #endregion
        #region Max Karakter Uzunluğu
        public const string TitleMaxKarakter = "'Proje Adı' en fazla 250 karakter olmalıdır.";
        #endregion
    #endregion
}