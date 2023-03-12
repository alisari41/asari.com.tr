namespace asari.com.tr.Application.Features.TechnologyProjects.Queries.GetById;

public class GetByIdTechnologyProjectResponse 
{
    #region Tablo Id
    public int Id { get; set; }
    #endregion

    #region Teknoloji Tablosundan Alınacaklar
    public string TechnologyTitle { get; set; }
    public string TechnologyDescription { get; set; }
    public string? TechnologyImageUrl { get; set; }
    public string TechnologyContent { get; set; }
    #endregion

    #region Project Tablosundan Alınacaklar
    public string ProjectTitle { get; set; }
    public string ProjectDescription { get; set; }
    public string ProjectImageUrl { get; set; }
    public string ProjectContent { get; set; }
    public string? ProjectGithubLink { get; set; }
    public string? ProjectFolderUrl { get; set; }
    public DateTime? ProjectCreateDate { get; set; }
    #endregion

    #region Programlama Dili Tablosundan Alınacaklar
    public ICollection<ProgrammingLanguageDto> ProgrammingLanguageDtos { get; set; }
    public class ProgrammingLanguageDto
    {
        public string ProgrammingLanguageName { get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    }
    #endregion

    #region Programlama Dili Teknolojisi Tablosundan Alınacaklar
    public ICollection<ProgrammingLanguageTechnologyDto> ProgrammingLanguageTechnologyDtos { get; set; }
    public class ProgrammingLanguageTechnologyDto
    {
        public string ProgrammingLanguageTechnologyName { get; set; } // Programlama Teknoloji Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    }
    #endregion
}