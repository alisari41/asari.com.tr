using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class EducationSkill : Entity
{
    public int SkillId { get; set; }
    public int EducationId { get; set; }

    public virtual Skill? Skill { get; set; }
    public virtual Education? Education { get; set; }

    public EducationSkill()
    {

    }

    public EducationSkill(int id, int skillId, int educationId) : this()
    {
        Id = id;
        SkillId = skillId;
        EducationId = educationId;
    }
}