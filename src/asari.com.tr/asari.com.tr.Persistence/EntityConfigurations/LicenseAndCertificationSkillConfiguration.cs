using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class LicenseAndCertificationSkillConfiguration : IEntityTypeConfiguration<LicenseAndCertificationSkill>
{
    public void Configure(EntityTypeBuilder<LicenseAndCertificationSkill> builder)
    {
        builder.ToTable("LicenseAndCertificationSkills").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.SkillId).HasColumnName("SkillId");
        builder.Property(p => p.LicenseAndCertificationId).HasColumnName("LicenseAndCertificationId");

        #region İlişkiler
        builder.HasOne(p => p.Skill);
        builder.HasOne(p => p.LicenseAndCertification);
        #endregion
    }
}
