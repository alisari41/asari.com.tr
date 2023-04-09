namespace asari.com.tr.Application.Features.Technologies.Constants;

public class TechnologyMessages
{
    #region İş Kuralı - Business Role
    public const string TeknolojiMevcutDegil = "Teknoloji mevcut değildir.";
    public const string TeknolojiMevcut = "Teknoloji sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Id'si boş olmamalıdır.";
        public const string TitleBosOlmamali = "'Başlık' boş olmamalıdır.";
        public const string DescriptionBosOlmamali = "'Açıklama' boş olmamalıdır.";
        public const string ContentBosOlmamali = "'İçerik' boş olmamalıdır.";
        #endregion
        #region Max Karakter Uzunluğu
        public const string TitleMaxKarakter = "Eğitim Başlığı en fazla 250 karakter olmalıdır.";
        #endregion
    #endregion
}