using Application.Tests.Constants;
using Application.Tests.Features.EducationSkills.Constants;
using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.EducationSkills.Queries.GetById;
using Core.CrossCuttingConcerns.Exceptions.Types;
using static asari.com.tr.Application.Features.EducationSkills.Queries.GetById.GetByIdEducationSkillQuery;
using Xunit;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.EducationSkills.Constants;

namespace Application.Tests.Features.EducationSkills.Queries.GetById;

public class GetByIdEducationSkillTests : EducationSkillMockRepository
{
    private readonly GetByIdEducationSkillQuery _query;
    private readonly GetByIdEducationSkillQueryHandler _handler;

    public GetByIdEducationSkillTests(EducationSkillFakeData fakeData, GetByIdEducationSkillQuery query) : base(fakeData)
    {
        _query = query;
        _handler = new GetByIdEducationSkillQueryHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.VeriAramaCategori)]
    public async Task EducationSkillIdsineGoreAramaTesti()
    {
        _query.Id = EducationSkillTestData.UpdateId;
        GetByIdEducationSkillResponse result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: EducationSkillTestData.UpdateId, result.Id);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task EducationSkillTablosundaOlmayanVeriyiAramaTesti()
    {
        _query.Id = EducationSkillTestData.NonexistentId;
        var exception = await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_query, CancellationToken.None));

        Assert.Contains(EducationSkillMessages.EgitimYetenegiMevcutDegil, exception.Message);
    }
}