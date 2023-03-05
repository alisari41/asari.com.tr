using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetList;

public class GetListProgrammingLanguageListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}