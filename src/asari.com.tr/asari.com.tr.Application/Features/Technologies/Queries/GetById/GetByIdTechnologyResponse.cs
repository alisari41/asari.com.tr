namespace asari.com.tr.Application.Features.Technologies.Queries.GetById;

public class GetByIdTechnologyResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Content { get; set; }


    #region Proje Tablosundan Alınacaklar
    public ICollection<ProjectDto> ProjectDtos { get; set; }
    public class ProjectDto
    {
        public int ProjectId { get; set; } // ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
        public string ProjectTitle { get; set; }
    }
    #endregion
}