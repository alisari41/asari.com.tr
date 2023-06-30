namespace asari.com.tr.Application.Features.Projects.Queries.GetById;

public class GetByIdProjectResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
    public string? GithubLink { get; set; }
    public string? FolderUrl { get; set; }
    public DateTime? CreateDate { get; set; }


    #region Teknoloji Tablosundan Alınacaklar
    public ICollection<TechnologyDto> TechnologyDtos { get; set; }
    public class TechnologyDto
    {
        public int TechnologyId { get; set; } // ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
        public string TechnologyTitle { get; set; }
    }
    #endregion

    #region Yetenek Tablosundan Alınacaklar
    public ICollection<SkillDto> SkillDtos { get; set; }
    public class SkillDto
    {
        public int SkillId { get; set; } // ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
        public string SkillName { get; set; }
    }
    #endregion
}