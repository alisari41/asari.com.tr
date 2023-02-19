using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Persistence.Contexts;
using asari.com.tr.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace asari.com.tr.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("Asari.com.trConnectionString")));// Projenin Adı Sonrasında ConnectionString

        #region Repositorylerin Bağlanması
        services.AddScoped<IProgrammingLanguageRepository, ProgrammingLanguageRepository>(); // Eğer Biri IProgrammingLanguageRepository isterse ona ProgrammingLanguageRepository ver 
        services.AddScoped<IProgrammingLanguageTechnologyRepository, ProgrammingLanguageTechnologyRepository>();
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectProgrammingLanguageRepository, ProjectProgrammingLanguageRepository>();
        services.AddScoped<ITecgnologyProjectRepository, TecgnologyProjectRepository>();
        services.AddScoped<ITechnologyRepository, TechnologyRepository>();
        services.AddScoped<IProjectSkillRepository, ProjectSkillRepository>();
        services.AddScoped<ISkillRepository, SkillRepository>();
        services.AddScoped<ILicenseAndCertificationSkillRepository, LicenseAndCertificationSkillRepository>();
        services.AddScoped<ILicenseAndCertificationRepository, LicenseAndCertificationRepository>();
        services.AddScoped<IEducationSkillRepository, EducationSkillRepository>();
        services.AddScoped<IEducationRepository, EducationRepository>();
        services.AddScoped<IExperienceSkillRepository, ExperienceSkillRepository>();
        services.AddScoped<IExperienceRepository, ExperienceRepository>();
        #endregion


        return services;
    }
}