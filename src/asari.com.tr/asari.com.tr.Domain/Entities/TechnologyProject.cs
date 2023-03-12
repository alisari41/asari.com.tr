using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class TechnologyProject : Entity
{
    public int TechnologyId { get; set; }
    public int ProjectId { get; set; }

    public virtual Technology? Technology { get; set; }
    public virtual Project? Project { get; set; }

    public TechnologyProject()
    {

    }

    public TechnologyProject(int id, int technologyId, int projectId) : this()
    {
        Id = id;
        TechnologyId = technologyId;
        ProjectId = projectId;
    }
}