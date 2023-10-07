using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.EducationSkills.Commands.Create;
using asari.com.tr.Application.Features.EducationSkills.Commands.Delete;
using asari.com.tr.Application.Features.EducationSkills.Commands.Update;
using asari.com.tr.Application.Features.EducationSkills.Queries.GetById;
using asari.com.tr.Application.Features.EducationSkills.Queries.GetList;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Tests.DependencyResolvers;

public static class EducationSkillServiceRegistration
{
    public static void AddEducationSkillServices(this IServiceCollection services)
    {
        services.AddTransient<EducationSkillFakeData>();
        services.AddTransient<CreateEducationSkillCommand>();
        services.AddTransient<DeleteEducationSkillCommand>();
        services.AddTransient<UpdateEducationSkillCommand>();
        services.AddTransient<GetByIdEducationSkillQuery>();
        services.AddTransient<GetListEducationSkillQuery>();

        #region FluentValidation
        services.AddTransient<CreateEducationSkillCommandValidator>();
        services.AddTransient<DeleteEducationSkillCommandValidator>();
        services.AddTransient<UpdateEducationSkillCommandValidator>();
        #endregion
    }
}