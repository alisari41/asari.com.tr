using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.Skills.Queries.GetList;

public class GetListSkillListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double? Degree { get; set; }
}