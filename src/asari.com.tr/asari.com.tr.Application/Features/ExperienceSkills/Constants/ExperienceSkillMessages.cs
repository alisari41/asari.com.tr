namespace asari.com.tr.Application.Features.ExperienceSkills.Constants;

public static class ExperienceSkillMessages
{
    #region İş Kuralı - Business Role
    public const string DeneyimYetenegiMevcutDegil = "Deneyim Yeteneği mevcut değildir.";
    public const string DeneyimYetenegiMevcut = "Deneyim Yeteneği sistemde zaten mevcuttur.";
    #endregion
    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Deneyim Yetenek Id' boş olmamalıdır.";
        public const string ExperienceIdBosOlmamali = "'Deneyim Id' boş olmamalıdır.";
        public const string SkillIdBosOlmamali = "'Yetenek Id' boş olmamalıdır.";
        #endregion
    #endregion
}