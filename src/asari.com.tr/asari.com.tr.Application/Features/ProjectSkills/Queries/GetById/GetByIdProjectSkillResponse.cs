namespace asari.com.tr.Application.Features.ProjectSkills.Queries.GetById;

public class GetByIdProjectSkillResponse
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz

    #region Tablo Id
    public int Id { get; set; }
    #endregion

    #region Project Tablosundan Alınacaklar
    public int ProjectId { get; set; }
    public string ProjectTitle { get; set; }
    #endregion

    #region Yetenek Tablosundan Alınacaklar
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    #endregion
}