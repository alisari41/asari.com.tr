using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class ProjectSkill : Entity
{
    public int SkillId { get; set; }
    public int ProjectId { get; set; }

    public virtual Skill? Skill { get; set; }
    public virtual Project? Project { get; set; }

    public ProjectSkill()
    {

    }

    public ProjectSkill(int id, int skillId, int projectId) : this()
    {
        Id = id;
        SkillId = skillId;
        ProjectId = projectId;
    }
}