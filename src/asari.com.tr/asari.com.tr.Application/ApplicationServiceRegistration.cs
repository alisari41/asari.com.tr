using asari.com.tr.Application.Features.ProgrammingLanguages.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace asari.com.tr.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Application Katmanı ile ilgli bütün injectionlarımızı yaptığımız yer

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());


        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly()); // Fluent Validation: Bir nesnenin özelliklerinin iş kurallarına dahil etmek için format uygunluğu ile ilgili
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>)); // Rol Bazlı Yetkilendirme
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>)); // Ön Belleğe atma
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>)); // Ön belleği temizleme
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>)); // Loglama  

        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));


        #region İş Kuralllarının Servislseri
        services.AddScoped<ProgrammingLanguageRules>(); // Business Kuralları bir kere bellekte durur.

        #endregion


        return services;
    }
}
