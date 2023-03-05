using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetList;

public class GetListProgrammingLanguageTechnologyListItemDto : IDto
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProgrammingLanguageName { get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
}