using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class Skill : Entity
{
    public string Name { get; set; }

    public virtual ICollection<ProjectSkill> ProjectSkills { get; set; }
    public virtual ICollection<ExperienceSkill> ExperienceSkills { get; set; }
    public virtual ICollection<EducationSkill> EducationSkills { get; set; }
    public virtual ICollection<LicenseAndCertificationSkill> LicenseAndCertificationSkills { get; set; }

    public Skill()
    {

    }

    public Skill(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}
