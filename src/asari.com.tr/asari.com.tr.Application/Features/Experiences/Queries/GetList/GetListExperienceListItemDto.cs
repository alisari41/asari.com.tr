using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.Experiences.Queries.GetList;

public class GetListExperienceListItemDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string EmploymentType { get; set; }
    public string CompanyName { get; set; }
    public string Location { get; set; }
    public bool Statu { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Industry { get; set; }
    public string Description { get; set; }
    public string? ProfileHeadline { get; set; }

    #region Skill Tablosundan Alınacaklar
    public ICollection<SkillDto> SkillDtos { get; set; }
    public class SkillDto
    {
        public int SkillId { get; set; } // ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
        public string SkillName { get; set; }
    }
    #endregion
}