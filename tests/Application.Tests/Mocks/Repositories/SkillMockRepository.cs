using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.Skills.Profiles;
using asari.com.tr.Application.Features.Skills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Test.Application.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class SkillMockRepository : BaseMockRepository<ISkillRepository, Skill, MappingProfiles, SkillBusinessRules, SkillFakeData>
{
    public SkillMockRepository(SkillFakeData fakeData) : base(fakeData)
    {

    }
}