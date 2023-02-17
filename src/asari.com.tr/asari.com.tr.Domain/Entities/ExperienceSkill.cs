using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class ExperienceSkill : Entity
{
    public int SkillId { get; set; }
    public int ExperienceId { get; set; }

    public virtual Skill? Skill { get; set; }
    public virtual Experience? Experience { get; set; }

    public ExperienceSkill()
    {

    }

    public ExperienceSkill(int id, int skillId, int experienceId) : this()
    {
        Id = id;
        SkillId = skillId;
        ExperienceId = experienceId;
    }
}