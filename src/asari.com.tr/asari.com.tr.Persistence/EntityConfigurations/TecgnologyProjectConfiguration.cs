using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class TecgnologyProjectConfiguration : IEntityTypeConfiguration<TecgnologyProject>
{
    public void Configure(EntityTypeBuilder<TecgnologyProject> builder)
    {
        builder.ToTable("TecgnologyProjects").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.TechnologyId).HasColumnName("TechnologyId");
        builder.Property(p => p.ProjectId).HasColumnName("ProjectId");

        #region İlişkiler
        builder.HasOne(p => p.Technology);
        builder.HasOne(p => p.Project);
        #endregion
    }
}
