using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class ProgrammingLanguageTechnologyConfiguration : IEntityTypeConfiguration<ProgrammingLanguageTechnology>
{
    public void Configure(EntityTypeBuilder<ProgrammingLanguageTechnology> builder)
    {
        builder.ToTable("ProgrammingLanguageTechnologies").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId"); // FK ( yabancıl anahtar / foreing key )
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();

        #region İlişkiler
        #region Has One
        builder.HasOne(p => p.ProgrammingLanguage); // Bir teknolojinin Bir programlama dili olur 
        #endregion
        #region Has Many
        builder.HasMany(p => p.ProjectProgrammingLanguageTechnologies);
        #endregion
        #endregion
    }
}
