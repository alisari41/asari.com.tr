using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.Technologies.Queries.GetList;

public class GetListTechnologyListItemDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Content { get; set; }

    // Altına Eklenecek sınıflardaki değişkenler eklenebilir.
}
