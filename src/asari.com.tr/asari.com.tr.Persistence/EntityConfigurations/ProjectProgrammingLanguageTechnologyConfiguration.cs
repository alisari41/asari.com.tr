using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class ProjectProgrammingLanguageTechnologyConfiguration : IEntityTypeConfiguration<ProjectProgrammingLanguageTechnology>
{
    public void Configure(EntityTypeBuilder<ProjectProgrammingLanguageTechnology> builder)
    {
        builder.ToTable("ProjectProgrammingLanguageTechnologies").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.ProgrammingLanguageTechnologyId).HasColumnName("ProgrammingLanguageTechnologyId");
        builder.Property(p => p.ProjectId).HasColumnName("ProjectId");

        #region İlişkiler
        builder.HasOne(p => p.ProgrammingLanguageTechnology);
        builder.HasOne(p => p.Project);
        #endregion
    }
}
