namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetById;

public class GetByIdProjectProgrammingLanguageTechnologyResponse
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz
    #region Tablo Id
    public int Id { get; set; }
    #endregion

    #region Project Tablosundan Alınacaklar
    public int ProjectId{ get; set; }
    public string ProjectTitle { get; set; }
    public string ProjectDescription { get; set; }
    public string ProjectImageUrl { get; set; }
    public string ProjectContent { get; set; }
    public string? ProjectGithubLink { get; set; }
    public string? ProjectFolderUrl { get; set; }
    public DateTime? ProjectCreateDate { get; set; }
    #endregion

    #region Programlama Dili Tablosundan Alınacaklar
    public string ProgrammingLanguageName { get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    #endregion

    #region Programlama Dili Teknolojisi Tablosundan Alınacaklar
    public int ProgrammingLanguageTechnologyId{ get; set; } // Programlama Teknoloji Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    public string ProgrammingLanguageTechnologyName { get; set; } // Programlama Teknoloji Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    #endregion

}