using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.Skills.Queries.GetList;

public class GetListSkillListItemDto : IDto
{
    #region Yetenek Tablosu
    public int Id { get; set; }
    public string Name { get; set; }
    public double? Degree { get; set; }
    #endregion

    #region Project Tablosundan Alınacaklar
    public ICollection<ProjectDto> ProjectDtos { get; set; }
    public class ProjectDto
    {
        public int ProjectId { get; set; } // ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
        public string ProjectTitle { get; set; }
    }
    #endregion

    #region Lisans ve Sertifikalar Tablosundan Alınacaklar
    public ICollection<LicenseAndCertificationDto> LicenseAndCertificationDtos { get; set; }
    public class LicenseAndCertificationDto
    {
        public int LicenseAndCertificationId { get; set; } // ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
        public string LicenseAndCertificationName { get; set; }
        public string LicenseAndCertificationUrl{ get; set; }
    }
    #endregion
}