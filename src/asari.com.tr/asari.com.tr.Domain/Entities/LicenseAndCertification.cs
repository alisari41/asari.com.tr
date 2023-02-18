using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class LicenseAndCertification : Entity
{
    public string Name { get; set; }
    public string IssuingOrganization { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? ImagegUrl { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }

    public virtual ICollection<LicenseAndCertificationSkill> LicenseAndCertificationSkills { get; set; }

    public LicenseAndCertification()
    {

    }

    public LicenseAndCertification(int id, string name, string issuingOrganization, DateTime? issueDate, DateTime? expirationDate, string? imageUrl, string? credentialId, string? credentialUrl)
    {
        Id = id;
        Name = name;
        IssuingOrganization = issuingOrganization;
        IssueDate = issueDate;
        ExpirationDate = expirationDate;
        ImagegUrl = imageUrl;
        CredentialId = credentialId;
        CredentialUrl = credentialUrl;
    }
}