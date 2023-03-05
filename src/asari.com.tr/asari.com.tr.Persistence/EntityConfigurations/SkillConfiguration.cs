using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class SkillConfiguration : IEntityTypeConfiguration<Skill>
{
    public void Configure(EntityTypeBuilder<Skill> builder)
    {
        builder.ToTable("Skills").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();

        //a.HasAlternateKey(p => p.Name); // Benzersiz alan

        #region İlişkiler
        builder.HasMany(p => p.ProjectSkills);
        builder.HasMany(p => p.ExperienceSkills);
        builder.HasMany(p => p.EducationSkills);
        builder.HasMany(p => p.LicenseAndCertificationSkills);
        #endregion
    }
}
