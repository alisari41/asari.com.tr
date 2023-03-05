using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.ToTable("Projects").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Title).HasColumnName("Title").IsRequired();
        builder.Property(p => p.Description).HasColumnName("Description").IsRequired();
        builder.Property(p => p.ImageUrl).HasColumnName("ImageUrl").IsRequired(false);
        builder.Property(p => p.Content).HasColumnName("Content").IsRequired();
        builder.Property(p => p.GithubLink).HasColumnName("GithubLink").IsRequired(false);
        builder.Property(p => p.FolderUrl).HasColumnName("FolderUrl").IsRequired(false);
        builder.Property(p => p.CreateDate).HasColumnName("CreateDate").IsRequired();

        #region İlişkiler
        builder.HasMany(p => p.ProjectProgrammingLanguageTechnologies);
        builder.HasMany(p => p.TecgnologyProjects);
        builder.HasMany(p => p.ProjectSkills);
        #endregion
    }
}
