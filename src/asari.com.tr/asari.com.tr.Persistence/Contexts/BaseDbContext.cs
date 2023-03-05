using asari.com.tr.Domain.Entities;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace asari.com.tr.Persistence.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }

    #region Entity - Tablolar
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public DbSet<ProgrammingLanguageTechnology> ProgrammingLanguageTechnologies { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectProgrammingLanguageTechnology> ProjectProgrammingLanguageTechnologies { get; set; } //
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

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
