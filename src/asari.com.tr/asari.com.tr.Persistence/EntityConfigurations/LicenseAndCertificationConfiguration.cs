using asari.com.tr.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace asari.com.tr.Persistence.EntityConfigurations;

public class LicenseAndCertificationConfiguration : IEntityTypeConfiguration<LicenseAndCertification>
{
    public void Configure(EntityTypeBuilder<LicenseAndCertification> builder)
    {
        builder.ToTable("LicensesAndCertifications").HasKey(k => k.Id);
        builder.Property(p => p.Id).HasColumnName("Id");
        builder.Property(p => p.Name).HasColumnName("Name").IsRequired();
        builder.Property(p => p.IssuingOrganization).HasColumnName("IssuingOrganization").IsRequired();
        builder.Property(p => p.IssueDate).HasColumnName("IssueDate").IsRequired(false);
        builder.Property(p => p.ExpirationDate).HasColumnName("ExpirationDate").IsRequired(false);
        builder.Property(p => p.ImagegUrl).HasColumnName("ImagegUrl").IsRequired(false);
        builder.Property(p => p.CredentialId).HasColumnName("CredentialId").IsRequired(false);
        builder.Property(p => p.CredentialUrl).HasColumnName("CredentialUrl").IsRequired(false);

        //a.HasAlternateKey(p => new { p.Name, p.CredentialId, p.CredentialUrl }); // Benzersiz alanlar

        #region İlişkiler
        builder.HasMany(p => p.LicenseAndCertificationSkills);
        #endregion
    }
}
