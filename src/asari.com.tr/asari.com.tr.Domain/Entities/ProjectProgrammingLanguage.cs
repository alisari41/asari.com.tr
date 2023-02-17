using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class ProjectProgrammingLanguage : Entity
{
    public int ProgrammingLanguageId { get; set; }
    public int ProjectId { get; set; }

    public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; }
    public virtual Project? Project { get; set; }

    public ProjectProgrammingLanguage()
    {

    }

    public ProjectProgrammingLanguage(int id, int programmingLanguageId, int projectId) : this()
    {
        Id = id;
        ProgrammingLanguageId = programmingLanguageId;
        ProjectId = projectId;
    }
}
