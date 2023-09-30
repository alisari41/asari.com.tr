using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.Skills.Commands.Create;
using asari.com.tr.Application.Features.Skills.Commands.Delete;
using asari.com.tr.Application.Features.Skills.Commands.Update;
using asari.com.tr.Application.Features.Skills.Queries.GetById;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests.DependencyResolvers;

public static class SkillServiceRegistration
{
    public static void AddSkillServices(this IServiceCollection services)
    {
        services.AddTransient<SkillFakeData>();
        services.AddTransient<CreateSkillCommand>();
        services.AddTransient<DeleteSkillCommand>();
        services.AddTransient<UpdateSkillCommand>();
        services.AddTransient<GetByIdSkillQuery>();
        services.AddTransient<GetListSkillQuery>();

        #region FluentValidation
        services.AddTransient<CreateSkillCommandValidator>();
        services.AddTransient<DeleteSkillCommandValidator>();
        services.AddTransient<UpdateSkillCommandValidator>();
        #endregion
    }
}