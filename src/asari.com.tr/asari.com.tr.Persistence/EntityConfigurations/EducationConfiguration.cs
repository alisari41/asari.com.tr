using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class EducationConfiguration : IEntityTypeConfiguration<Education>
{
    public void Configure(EntityTypeBuilder<Education> builder)
    {
        builder.ToTable("Educations").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
        builder.Property(p => p.Degree).HasColumnName("Degree").IsRequired();
        builder.Property(p => p.FieldOfStudy).HasColumnName("FieldOfStudy").IsRequired();
        builder.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
        builder.Property(p => p.EndDateOrExcepted).HasColumnName("EndDateOrExcepted").IsRequired(false);
        builder.Property(p => p.Grade).HasColumnName("Grade").IsRequired(false);
        builder.Property(p => p.ActivityAndCommunity).HasColumnName("ActivityAndCommunity").IsRequired(false);
        builder.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
        builder.Property(p => p.MediaUrl).HasColumnName("MediaUrl").IsRequired(false);

        //a.HasAlternateKey(p => p.Name); // Benzersiz alanlar

        #region İlişkiler
        builder.HasMany(p => p.EducationSkills);
        #endregion
    }
}
