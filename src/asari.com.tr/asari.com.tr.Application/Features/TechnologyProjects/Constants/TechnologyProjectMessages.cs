namespace asari.com.tr.Application.Features.TechnologyProjects.Constants;

public static class TechnologyProjectMessages
{
    #region İş Kuralı - Business Role
    public const string TeknolojiProjeMevcutDegil = "Teknoloji Projesi mevcut değildir.";
    public const string TeknolojiProjeMevcut = "Teknoloji Projesi sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Teknoloji Proje Id' boş olmamalıdır.";
        public const string TechnologyIdBosOlmamali = "'Teknoloji Id' boş olmamalıdır.";
        public const string ProjectIdBosOlmamali = "'Proje Id' boş olmamalıdır.";
        #endregion
    #endregion
}