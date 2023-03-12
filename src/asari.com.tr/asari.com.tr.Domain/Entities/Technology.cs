using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class Technology : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string? ImageUrl { get; set; }
    public string Content { get; set; }

    public virtual ICollection<TechnologyProject> TecgnologyProjects { get; set; }

    public Technology()
    {

    }

    public Technology(int id, string title, string description, string imageUrl, string content) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        Content = content;
    }
}
