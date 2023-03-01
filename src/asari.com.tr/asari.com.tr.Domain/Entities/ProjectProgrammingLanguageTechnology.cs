using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class ProjectProgrammingLanguageTechnology : Entity
{
    public int ProgrammingLanguageTechnologyId { get; set; }
    public int ProjectId { get; set; }

    public virtual ProgrammingLanguageTechnology? ProgrammingLanguageTechnology { get; set; }
    public virtual Project? Project { get; set; }

    public ProjectProgrammingLanguageTechnology()
    {

    }

    public ProjectProgrammingLanguageTechnology(int id, int programmingLanguageTechnologyId, int projectId) : this()
    {
        Id = id;
        ProgrammingLanguageTechnologyId = programmingLanguageTechnologyId;
        ProjectId = projectId;
    }
}