namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetById;

public class GetByIdProgrammingLanguageTechnologyResponse
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz
    public int Id { get; set; }
    public string Name { get; set; }
    public int ProgrammingLanguageId{ get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
    public string ProgrammingLanguageName { get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
}