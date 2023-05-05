namespace asari.com.tr.Application.Features.EducationSkills.Queries.GetById;

public class GetByIdEducationSkillGetByIdResponse
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz

    #region Tablo Id
    public int Id { get; set; }
    #endregion

    #region Eğitim Tablosundan Alınacaklar
    public int EducationId { get; set; }
    public string EducationName { get; set; }
    #endregion

    #region Yetenek Tablosundan Alınacaklar
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    #endregion
}