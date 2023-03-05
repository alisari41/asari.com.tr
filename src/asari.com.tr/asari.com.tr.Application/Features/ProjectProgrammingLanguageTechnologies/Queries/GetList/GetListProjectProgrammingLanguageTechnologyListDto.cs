using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetList;

public class GetListProjectProgrammingLanguageTechnologyListDto : IDto
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz
    #region Tablo Id
    public int Id { get; set; }
    #endregion

    #region Project Tablosundan Alınacaklar
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
    public string? GithubLink { get; set; }
    public string? FolderUrl { get; set; }
    public DateTime? CreateDate { get; set; }
    #endregion

    #region Programlama Dili Tablosundan Alınacaklar
    public string ProgrammingLanguageName { get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    #endregion

    #region Programlama Dili Teknolojisi Tablosundan Alınacaklar
    public string ProgrammingLanguageTechnologyName { get; set; } // Programlama Teknoloji Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    #endregion
}