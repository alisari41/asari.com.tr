using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class TecgnologyProject : Entity
{
    public int TechnologyId { get; set; }
    public int ProjectId { get; set; }

    public virtual Technology? Technology { get; set; }
    public virtual Project? Project { get; set; }

    public TecgnologyProject()
    {

    }

    public TecgnologyProject(int id, int technologyId, int projectId) : this()
    {
        Id = id;
        TechnologyId = technologyId;
        ProjectId = projectId;
    }
}