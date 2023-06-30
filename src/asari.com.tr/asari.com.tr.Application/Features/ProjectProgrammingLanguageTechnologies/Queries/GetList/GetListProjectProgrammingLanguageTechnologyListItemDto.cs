using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetList;

public class GetListProjectProgrammingLanguageTechnologyListItemDto : IDto
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz
    #region Tablo Id
    public int Id { get; set; }
    #endregion

    #region Project Tablosundan Alınacaklar
    public int ProjectId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
    public string? GithubLink { get; set; }
    public string? FolderUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    #endregion

    #region Programlama Dili Tablosundan Alınacaklar
    public int ProgrammingLanguageId{ get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    public string ProgrammingLanguageName { get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    #endregion

    #region Programlama Dili Teknolojisi Tablosundan Alınacaklar
    public int ProgrammingLanguageTechnologyId{ get; set; } // Programlama Teknoloji Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    public string ProgrammingLanguageTechnologyName { get; set; } // Programlama Teknoloji Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    #endregion

    #region Yetenek Tablosundan Alınacaklar
    public ICollection<SkillDto> SkillDtos { get; set; }
    public class SkillDto
    {
        public int SkillId { get; set; } // ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
        public string SkillName{ get; set; }
    }
    #endregion
}