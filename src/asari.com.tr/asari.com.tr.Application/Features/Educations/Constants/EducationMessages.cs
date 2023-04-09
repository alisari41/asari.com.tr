namespace asari.com.tr.Application.Features.Educations.Constants;

public static class EducationMessages
{
    #region İş Kuralı - Business Role
    public const string EgitimMevcutDegil = "Eğitim mevcut değildir.";
    #endregion

    #region Formatlama - Fluent Validation
        #region Zorunlu Alanlar
        public const string IdBosOlmamali = "'Id'si boş olmamalıdır.";
        public const string NameBosOlmamali = "Eğitim Başlığı boş olmamalıdır.";
        public const string DegreeBosOlmamali = "Eğitim Derecesi boş olmamalıdır.";
        public const string FieldOfStudyBosOlmamali = "Bölüm boş olmamalıdır.";
        public const string StartDateBosOlmamali = "Başlangıç Tarihi boş olmamalıdır.";
        #endregion
        #region Max Karakter Uzunluğu
        public const string NameMaxKarakter = "Eğitim Başlığı en fazla 250 karakter olmalıdır.";
        public const string FieldOfStudyMaxKarakter = "'Bölüm' alanı en fazla 100 karakter olmalıdır.";
        public const string GradeMaxKarakter = "'Not' alanı en fazla 250 karakter olmalıdır.";
        public const string ActivityAndCommunityMaxKarakter = "'Faaliyet ve topluluklar' alanı en fazla 500 karakter olmalıdır.";
        public const string DescriptionMaxKarakter = "'Açıklama' alanı en fazla 1000 karakter olmalıdır.";
        #endregion
    #endregion
}