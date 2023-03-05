using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class ExperienceConfiguration : IEntityTypeConfiguration<Experience>
{
    public void Configure(EntityTypeBuilder<Experience> builder)
    {
        builder.ToTable("Experiences").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Title).HasColumnName("Title").IsRequired();
        builder.Property(p => p.EmploymentType).HasColumnName("EmploymentType").IsRequired();
        builder.Property(p => p.CompanyName).HasColumnName("CompanyName").IsRequired();
        builder.Property(p => p.Location).HasColumnName("Location").IsRequired();
        builder.Property(p => p.Statu).HasColumnName("Statu").IsRequired();
        builder.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
        builder.Property(p => p.EndDate).HasColumnName("EndDate").IsRequired(false);
        builder.Property(p => p.Industry).HasColumnName("Industry").IsRequired();
        builder.Property(p => p.Description).HasColumnName("Description").IsRequired();
        builder.Property(p => p.ProfileHeadline).HasColumnName("ProfileHeadline").IsRequired(false);

        #region İlişkiler
        builder.HasMany(p => p.ExperienceSkills);
        #endregion
    }
}
