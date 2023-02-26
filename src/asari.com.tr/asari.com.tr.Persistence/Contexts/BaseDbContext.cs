using asari.com.tr.Domain.Entities;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace asari.com.tr.Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    #region Entity - Tablolar
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public DbSet<ProgrammingLanguageTechnology> ProgrammingLanguageTechnologies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectProgrammingLanguage> ProjectProgrammingLanguages { get; set; } //
    public DbSet<TecgnologyProject> TecgnologyProjects { get; set; } //
    public DbSet<Technology> Technologies { get; set; }
    public DbSet<ProjectSkill> ProjectSkills { get; set; } //
    public DbSet<Skill> Skills { get; set; }
    public DbSet<LicenseAndCertificationSkill> LicenseAndCertificationSkills { get; set; } //
    public DbSet<LicenseAndCertification> LicensesAndCertifications { get; set; }
    public DbSet<EducationSkill> EducationSkills { get; set; } //
    public DbSet<Education> Educations { get; set; }
    public DbSet<ExperienceSkill> ExperienceSkills { get; set; } //
    public DbSet<Experience> Experiences { get; set; }
    #endregion

    #region JWT (JSON WEB TOKEN)
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    #endregion

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //    base.OnConfiguring(
        //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Sql de dahada geliştirme yapılmak istenirse "EF Fluent Mapping" yazarak bakabiliriz

        modelBuilder.Entity<ProgrammingLanguage>(a =>
        {
            a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name").IsRequired();
            //a.HasAlternateKey(p => p.Name); // Benzersiz alan

            #region İlişkiler
            a.HasMany(p => p.ProgrammingLanguageTechnologies); // Bir programlama dilinin birden fazla teknolojisi olabileceği için bu şekilde yazıldı
            a.HasMany(p => p.ProjectProgrammingLanguages);
            #endregion
        });

        modelBuilder.Entity<ProgrammingLanguageTechnology>(a =>
        {
            a.ToTable("ProgrammingLanguageTechnologies").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId"); // FK ( yabancıl anahtar / foreing key )
            a.Property(p => p.Name).HasColumnName("Name").IsRequired();
            //a.HasAlternateKey(p => p.Name); // Benzersiz alan

            #region İlişkiler
            a.HasOne(p => p.ProgrammingLanguage); // Bir teknolojinin Bir programlama dili olur 
            #endregion
        });

        modelBuilder.Entity<Project>(a =>
        {
            a.ToTable("Projects").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Title).HasColumnName("Title").IsRequired();
            a.Property(p => p.Description).HasColumnName("Description").IsRequired();
            a.Property(p => p.ImageUrl).HasColumnName("ImageUrl").IsRequired(false);
            a.Property(p => p.Content).HasColumnName("Content").IsRequired();
            a.Property(p => p.GithubLink).HasColumnName("GithubLink").IsRequired(false);
            a.Property(p => p.FolderUrl).HasColumnName("FolderUrl").IsRequired(false);
            a.Property(p => p.CreateDate).HasColumnName("CreateDate").IsRequired();

            //a.HasAlternateKey(p => new { p.Title, p.GithubLink }); // Benzersiz alanlar

            #region İlişkiler
            a.HasMany(p => p.ProjectProgrammingLanguages);
            a.HasMany(p => p.TecgnologyProjects);
            a.HasMany(p => p.ProjectSkills);
            #endregion
        });

        modelBuilder.Entity<ProjectProgrammingLanguage>(a =>
        {
            a.ToTable("ProjectProgrammingLanguages").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
            a.Property(p => p.ProjectId).HasColumnName("ProjectId");

            #region İlişkiler
            a.HasOne(p => p.ProgrammingLanguage);
            a.HasOne(p => p.Project);
            #endregion
        });

        modelBuilder.Entity<TecgnologyProject>(a =>
        {
            a.ToTable("TecgnologyProjects").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.TechnologyId).HasColumnName("TechnologyId");
            a.Property(p => p.ProjectId).HasColumnName("ProjectId");

            #region İlişkiler
            a.HasOne(p => p.Technology);
            a.HasOne(p => p.Project);
            #endregion
        });

        modelBuilder.Entity<Technology>(a =>
        {
            a.ToTable("Technologies").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Title).HasColumnName("Title").IsRequired();
            a.Property(p => p.Description).HasColumnName("Description").IsRequired();
            a.Property(p => p.ImageUrl).HasColumnName("ImageUrl").IsRequired(false);
            a.Property(p => p.Content).HasColumnName("Content").IsRequired();

            #region İlişkiler
            a.HasMany(p => p.TecgnologyProjects);
            #endregion
        });

        modelBuilder.Entity<ProjectSkill>(a =>
        {
            a.ToTable("ProjectSkills").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.SkillId).HasColumnName("SkillId");
            a.Property(p => p.ProjectId).HasColumnName("ProjectId");

            #region İlişkiler
            a.HasOne(p => p.Skill);
            a.HasOne(p => p.Project);
            #endregion
        });

        modelBuilder.Entity<Skill>(a =>
        {
            a.ToTable("Skills").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name").IsRequired();

            //a.HasAlternateKey(p => p.Name); // Benzersiz alan

            #region İlişkiler
            a.HasMany(p => p.ProjectSkills);
            a.HasMany(p => p.ExperienceSkills);
            a.HasMany(p => p.EducationSkills);
            a.HasMany(p => p.LicenseAndCertificationSkills);
            #endregion
        });

        modelBuilder.Entity<LicenseAndCertificationSkill>(a =>
        {
            a.ToTable("LicenseAndCertificationSkills").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.SkillId).HasColumnName("SkillId");
            a.Property(p => p.LicenseAndCertificationId).HasColumnName("LicenseAndCertificationId");

            #region İlişkiler
            a.HasOne(p => p.Skill);
            a.HasOne(p => p.LicenseAndCertification);
            #endregion
        });

        modelBuilder.Entity<LicenseAndCertification>(a =>
        {
            a.ToTable("LicensesAndCertifications").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name").IsRequired();
            a.Property(p => p.IssuingOrganization).HasColumnName("IssuingOrganization").IsRequired();
            a.Property(p => p.IssueDate).HasColumnName("IssueDate").IsRequired(false);
            a.Property(p => p.ExpirationDate).HasColumnName("ExpirationDate").IsRequired(false);
            a.Property(p => p.ImagegUrl).HasColumnName("ImagegUrl").IsRequired(false);
            a.Property(p => p.CredentialId).HasColumnName("CredentialId").IsRequired(false);
            a.Property(p => p.CredentialUrl).HasColumnName("CredentialUrl").IsRequired(false);

            //a.HasAlternateKey(p => new { p.Name, p.CredentialId, p.CredentialUrl }); // Benzersiz alanlar

            #region İlişkiler
            a.HasMany(p => p.LicenseAndCertificationSkills);
            #endregion
        });

        modelBuilder.Entity<EducationSkill>(a =>
        {
            a.ToTable("EducationSkills").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.SkillId).HasColumnName("SkillId");
            a.Property(p => p.EducationId).HasColumnName("EducationId");

            #region İlişkiler
            a.HasOne(p => p.Skill);
            a.HasOne(p => p.Education);
            #endregion
        });

        modelBuilder.Entity<Education>(a =>
        {
            a.ToTable("Educations").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name").IsRequired();
            a.Property(p => p.Degree).HasColumnName("Degree").IsRequired();
            a.Property(p => p.FieldOfStudy).HasColumnName("FieldOfStudy").IsRequired();
            a.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
            a.Property(p => p.EndDateOrExcepted).HasColumnName("EndDateOrExcepted").IsRequired(false);
            a.Property(p => p.Grade).HasColumnName("Grade").IsRequired(false);
            a.Property(p => p.ActivityAndCommunity).HasColumnName("ActivityAndCommunity").IsRequired(false);
            a.Property(p => p.Description).HasColumnName("Description").IsRequired(false);
            a.Property(p => p.MediaUrl).HasColumnName("MediaUrl").IsRequired(false);

            //a.HasAlternateKey(p => p.Name); // Benzersiz alanlar

            #region İlişkiler
            a.HasMany(p => p.EducationSkills);
            #endregion
        });

        modelBuilder.Entity<ExperienceSkill>(a =>
        {
            a.ToTable("ExperienceSkills").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.SkillId).HasColumnName("SkillId");
            a.Property(p => p.ExperienceId).HasColumnName("ExperienceId");

            #region İlişkiler
            a.HasOne(p => p.Skill);
            a.HasOne(p => p.Experience);
            #endregion
        });

        modelBuilder.Entity<Experience>(a =>
        {
            a.ToTable("Experiences").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Title).HasColumnName("Title").IsRequired();
            a.Property(p => p.EmploymentType).HasColumnName("EmploymentType").IsRequired();
            a.Property(p => p.CompanyName).HasColumnName("CompanyName").IsRequired();
            a.Property(p => p.Location).HasColumnName("Location").IsRequired();
            a.Property(p => p.Statu).HasColumnName("Statu").IsRequired();
            a.Property(p => p.StartDate).HasColumnName("StartDate").IsRequired();
            a.Property(p => p.EndDate).HasColumnName("EndDate").IsRequired(false);
            a.Property(p => p.Industry).HasColumnName("Industry").IsRequired();
            a.Property(p => p.Description).HasColumnName("Description").IsRequired();
            a.Property(p => p.ProfileHeadline).HasColumnName("ProfileHeadline").IsRequired(false);

            #region İlişkiler
            a.HasMany(p => p.ExperienceSkills);
            #endregion
        });
        modelBuilder.Entity<User>(a =>
        {
            a.ToTable("Users").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.FirstName).HasColumnName("FirstName");
            a.Property(p => p.LastName).HasColumnName("LastName");
            a.Property(p => p.Email).HasColumnName("Email");
            a.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
            a.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
            a.Property(p => p.Status).HasColumnName("Status");
            a.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");

            #region Diğer Tablolar ile bağlantı
            a.HasMany(p => p.UserOperationClaims);
            a.HasMany(p => p.RefreshTokens);
            #endregion
        });

        modelBuilder.Entity<OperationClaim>(a =>
        {
            a.ToTable("OperationClaims").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name");
        });

        modelBuilder.Entity<UserOperationClaim>(a =>
        {
            a.ToTable("UserOperationClaims").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.UserId).HasColumnName("UserId");
            a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");

            #region User ve OperationClaim ile Bağlantı
            a.HasOne(p => p.User);
            a.HasOne(p => p.OperationClaim);
            #endregion
        });

        modelBuilder.Entity<RefreshToken>(a =>
        {
            a.ToTable("RefreshTokens").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.UserId).HasColumnName("UserId");
            a.Property(p => p.Token).HasColumnName("Token");
            a.Property(p => p.Expires).HasColumnName("Expires");
            a.Property(p => p.Created).HasColumnName("Created");
            a.Property(p => p.CreatedByIp).HasColumnName("CreatedByIp");
            a.Property(p => p.Revoked).HasColumnName("Revoked");
            a.Property(p => p.RevokedByIp).HasColumnName("RevokedByIp");
            a.Property(p => p.ReplacedByToken).HasColumnName("ReplacedByToken");
            a.Property(p => p.ReasonRevoked).HasColumnName("ReasonRevoked");

            #region User ile Bağlantı
            a.HasOne(p => p.User);
            #endregion
        });
    }
}
