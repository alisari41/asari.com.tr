using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class EducationSkillConfiguration : IEntityTypeConfiguration<EducationSkill>
{
    public void Configure(EntityTypeBuilder<EducationSkill> builder)
    {
        builder.ToTable("EducationSkills").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.SkillId).HasColumnName("SkillId");
        builder.Property(p => p.EducationId).HasColumnName("EducationId");

        #region İlişkiler
        builder.HasOne(p => p.Skill);
        builder.HasOne(p => p.Education);
        #endregion
    }
}
