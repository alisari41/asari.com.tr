using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.Experiences.Commands.Create;
using asari.com.tr.Application.Features.Experiences.Commands.Delete;
using asari.com.tr.Application.Features.Experiences.Commands.Update;
using asari.com.tr.Application.Features.Experiences.Queries.GetById;
using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests.DependencyResolvers;

public static class ExperienceServiceRegistration
{
    public static void AddExperienceServices(this IServiceCollection services)
    {
        services.AddTransient<ExperienceFakeData>();
        services.AddTransient<CreateExperienceCommand>();
        services.AddTransient<DeleteExperienceCommand>();
        services.AddTransient<UpdateExperienceCommand>();
        services.AddTransient<GetByIdExperienceQuery>();
        services.AddTransient<GetListExperienceQuery>();

        #region FluentValidation
        services.AddTransient<CreateExperienceCommandValidator>();
        services.AddTransient<DeleteExperienceCommandValidator>();
        services.AddTransient<UpdateExperienceCommandValidator>();
        #endregion
    }
}