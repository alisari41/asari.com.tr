using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.EducationSkills.Profiles;
using asari.com.tr.Application.Features.EducationSkills.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Test.Application.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class EducationSkillMockRepository : BaseMockRepository<IEducationSkillRepository, EducationSkill, MappingProfiles, EducationSkillBusinessRules, EducationSkillFakeData>
{
    public EducationSkillMockRepository(EducationSkillFakeData fakeData) : base(fakeData)
    {
    }
}