namespace asari.com.tr.Application.Features.EducationSkills.Constants;

public static class EducationSkillMessages
{
    #region İş Kuralı - Business Role
    public const string EgitimYetenegiMevcutDegil = "Eğitim yeteneği mevcut değildir.";
    public const string EgitimYetenegiMevcut = "Eğitim Yeteneği sistemde zaten mevcuttur.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string EducationSkillIdBosOlmamali = "'Eğitim Yetenek Id'si boş olmamalıdır.";
        public const string EducationIdBosOlmamali = "'Eğitim Id'si boş olmamalıdır.";
        public const string SkillIdBosOlmamali = "'Yetenek Id'si boş olmamalıdır.";
        #endregion
    #endregion
}