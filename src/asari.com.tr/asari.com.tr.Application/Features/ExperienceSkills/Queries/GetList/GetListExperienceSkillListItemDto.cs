namespace asari.com.tr.Application.Features.ExperienceSkills.Queries.GetList;

public class GetListExperienceSkillListItemDto
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz

    #region Tablo Id
    public int Id { get; set; }
    #endregion

    #region Deneyim Tablosundan Alınacaklar
    public int ExperienceId { get; set; }
    public string ExperienceTitle { get; set; }
    #endregion

    #region Yetenek Tablosundan Alınacaklar
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    #endregion
}