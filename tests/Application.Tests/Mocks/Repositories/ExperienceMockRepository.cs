using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.Experiences.Profiles;
using asari.com.tr.Application.Features.Experiences.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Test.Application.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class ExperienceMockRepository : BaseMockRepository<IExperienceRepository, Experience, MappingProfiles, ExperienceBusinessRules, ExperienceFakeData>
{
    public ExperienceMockRepository(ExperienceFakeData experienceFakeData) : base(experienceFakeData)
    {

    }
}