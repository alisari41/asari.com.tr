using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class ProjectSkillConfiguration : IEntityTypeConfiguration<ProjectSkill>
{
    public void Configure(EntityTypeBuilder<ProjectSkill> builder)
    {
        builder.ToTable("ProjectSkills").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.SkillId).HasColumnName("SkillId");
        builder.Property(p => p.ProjectId).HasColumnName("ProjectId");

        #region İlişkiler
        builder.HasOne(p => p.Skill);
        builder.HasOne(p => p.Project);
        #endregion
    }
}
