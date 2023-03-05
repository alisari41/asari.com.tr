using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.Projects.Queries.GetList;

public class GetListProjectListItemDto : IDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
    public string? GithubLink { get; set; }
    public string? FolderUrl { get; set; }
    public DateTime? CreateDate { get; set; }
}