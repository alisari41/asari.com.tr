using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.Educations.Profiles;
using asari.com.tr.Application.Features.Educations.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Test.Application.Repositories;

namespace Application.Tests.Mocks.Repositories;

public class EducationMockRepository : BaseMockRepository<IEducationRepository, Education, MappingProfiles, EducationBusinessRules, EducationFakeData>
{
    public EducationMockRepository(EducationFakeData educationFakeData) : base(educationFakeData)
    {

    }
}