using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class Project : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Content { get; set; }
    public string? GithubLink { get; set; }
    public string? FolderUrl { get; set; }
    public DateTime? CreateDate { get; set; }

    public virtual ICollection<ProjectProgrammingLanguageTechnology> ProjectProgrammingLanguageTechnologies { get; set; }
    public virtual ICollection<TecgnologyProject> TecgnologyProjects { get; set; }
    public virtual ICollection<ProjectSkill> ProjectSkills { get; set; }

    public Project()
    {

    }

    public Project(int id, string title, string description, string imageUrl, string content, string? githubLink, string? folderUrl, DateTime? createDate) : this()
    {
        Id = id;
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        Content = content;
        GithubLink = githubLink;
        FolderUrl = folderUrl;
        CreateDate = createDate;
    }
}