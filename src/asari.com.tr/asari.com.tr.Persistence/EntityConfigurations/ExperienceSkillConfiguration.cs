using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class ExperienceSkillConfiguration : IEntityTypeConfiguration<ExperienceSkill>
{
    public void Configure(EntityTypeBuilder<ExperienceSkill> builder)
    {
        builder.ToTable("ExperienceSkills").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.SkillId).HasColumnName("SkillId");
        builder.Property(p => p.ExperienceId).HasColumnName("ExperienceId");

        #region İlişkiler
        builder.HasOne(p => p.Skill);
        builder.HasOne(p => p.Experience);
        #endregion
    }
}
