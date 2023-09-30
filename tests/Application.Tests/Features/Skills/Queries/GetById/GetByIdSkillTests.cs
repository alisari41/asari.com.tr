using Application.Tests.Constants;
using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.Skills.Queries.GetById;
using AutoMapper;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Moq;
using static asari.com.tr.Application.Features.Skills.Queries.GetById.GetByIdSkillQuery;
using Xunit;
using Application.Tests.Mocks.Repositories;
using Application.Tests.Features.Skills.Constants;

namespace Application.Tests.Features.Skills.Queries.GetById;

public class GetByIdSkillTests : SkillMockRepository
{
    private readonly GetByIdSkillQuery _query;
    private readonly GetByIdSkillQueryHandler _handler;

    public GetByIdSkillTests(SkillFakeData fakeData, GetByIdSkillQuery query) : base(fakeData)
    {
        _query = query;
        _handler = new GetByIdSkillQueryHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.VeriAramaCategori)]
    public async Task SkillIdsineGoreAramaTesti()
    {
        _query.Id = SkillTestData.UpdateId;
        GetByIdSkillResponse result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: SkillTestData.CreateName, result.Name);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task SkillTablosundaOlmayanVeriyiAramaTesti()
    {
        _query.Id = SkillTestData.NonexistentId;
        await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_query, CancellationToken.None));
    }

}