using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.Educations.Commands.Create;
using asari.com.tr.Application.Features.Educations.Commands.Delete;
using asari.com.tr.Application.Features.Educations.Commands.Update;
using asari.com.tr.Application.Features.Educations.Queries.GetById;
using asari.com.tr.Application.Features.Educations.Queries.GetList;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests.DependencyResolvers;

public static class EducationServiceRegistration
{
    public static void AddEducationServices(this IServiceCollection services)
    {
        services.AddTransient<EducationFakeData>();
        services.AddTransient<CreateEducationCommand>();
        services.AddTransient<DeleteEducationCommand>();
        services.AddTransient<UpdateEducationCommand>();
        services.AddTransient<GetByIdEducationQuery>();
        services.AddTransient<GetListEducationQuery>();

        #region FluentValidation
        services.AddTransient<CreateEducationCommandValidator>();
        services.AddTransient<DeleteEducationCommandValidator>();
        services.AddTransient<UpdateEducationCommandValidator>();
        #endregion
    }
}