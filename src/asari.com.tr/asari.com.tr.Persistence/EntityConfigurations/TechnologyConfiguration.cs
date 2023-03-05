using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class TechnologyConfiguration : IEntityTypeConfiguration<Technology>
{
    public void Configure(EntityTypeBuilder<Technology> builder)
    {
        builder.ToTable("Technologies").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Title).HasColumnName("Title").IsRequired();
        builder.Property(p => p.Description).HasColumnName("Description").IsRequired();
        builder.Property(p => p.ImageUrl).HasColumnName("ImageUrl").IsRequired(false);
        builder.Property(p => p.Content).HasColumnName("Content").IsRequired();

        #region İlişkiler
        builder.HasMany(p => p.TecgnologyProjects);
        #endregion
    }
}
