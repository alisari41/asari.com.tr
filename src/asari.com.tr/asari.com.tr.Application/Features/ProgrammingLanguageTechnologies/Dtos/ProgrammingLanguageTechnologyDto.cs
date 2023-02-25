namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Dtos;

public class ProgrammingLanguageTechnologyDto
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProgrammingLanguageName { get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
}
