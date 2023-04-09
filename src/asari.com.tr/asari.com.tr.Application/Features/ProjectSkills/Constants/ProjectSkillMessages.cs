namespace asari.com.tr.Application.Features.ProjectSkills.Constants;

public static class ProjectSkillMessages
{
    #region İş Kuralı - Business Role
    public const string ProjeYetenegiMevcutDegil = "Proje Yeteneği mevcut değildir.";
    public const string ProjeYetenegiMevcut = "Proje Yeteneği sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Proje Yetenek Id'si boş olmamalıdır.";
        public const string ProjectIdBosOlmamali = "'Proje Id'si boş olmamalıdır.";
        public const string SkillIdBosOlmamali = "'Yetenek Id'si boş olmamalıdır.";
        #endregion
    #endregion
}