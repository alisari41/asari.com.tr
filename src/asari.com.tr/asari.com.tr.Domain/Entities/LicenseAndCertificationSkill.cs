using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class LicenseAndCertificationSkill : Entity
{
    public int SkillId { get; set; }
    public int LicenseAndCertificationId { get; set; }

    public virtual Skill? Skill { get; set; }
    public virtual LicenseAndCertification? LicenseAndCertification { get; set; }

    public LicenseAndCertificationSkill()
    {

    }

    public LicenseAndCertificationSkill(int id, int skillId, int licenseAndCertificationId) : this()
    {
        Id = id;
        SkillId = skillId;
        LicenseAndCertificationId = licenseAndCertificationId;
    }
}