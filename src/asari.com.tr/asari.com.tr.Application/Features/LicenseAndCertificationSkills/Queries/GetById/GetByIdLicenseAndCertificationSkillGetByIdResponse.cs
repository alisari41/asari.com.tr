namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Queries.GetById;

public class GetByIdLicenseAndCertificationSkillGetByIdResponse
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz

    #region Tablo Id
    public int Id { get; set; }
    #endregion

    #region Lisans ve Sertifika Tablosundan Alınacaklar
    public int LicenseAndCertificationId { get; set; }
    public string LicenseAndCertificationName { get; set; }
    #endregion

    #region Yetenek Tablosundan Alınacaklar
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    #endregion
}