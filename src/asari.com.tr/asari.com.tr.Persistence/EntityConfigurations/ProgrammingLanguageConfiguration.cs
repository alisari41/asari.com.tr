using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class ProgrammingLanguageConfiguration : IEntityTypeConfiguration<ProgrammingLanguage>
{
    public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
    {
        builder.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();

        #region İlişkiler
        builder.HasMany(p => p.ProgrammingLanguageTechnologies); // Bir programlama dilinin birden fazla teknolojisi olabileceği için bu şekilde yazıldı
        #endregion
    }
}