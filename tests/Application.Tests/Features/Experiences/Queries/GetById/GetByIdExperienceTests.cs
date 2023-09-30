using Application.Tests.Constants;
using Application.Tests.Features.Experiences.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Experiences.Queries.GetById;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Xunit;
using static asari.com.tr.Application.Features.Experiences.Queries.GetById.GetByIdExperienceQuery;

namespace Application.Tests.Features.Experiences.Queries.GetById;

public class GetByIdExperienceTests : ExperienceMockRepository
{
    private readonly GetByIdExperienceQuery _query;
    private readonly GetByIdExperienceQueryHandler _handler;

    public GetByIdExperienceTests(ExperienceFakeData fakeData, GetByIdExperienceQuery query) : base(fakeData)
    {
        _query = query;
        _handler = new GetByIdExperienceQueryHandler(MockRepository.Object, Mapper, BusinessRules);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.VeriAramaCategori)]
    public async Task ExperienceIdsineGoreAramaTesti()
    {
        _query.Id = ExperienceTestData.UpdateId;
        GetByIdExperienceResponse result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: ExperienceTestData.CreateTitle, result.Title);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task ExperienceTablosundaOlmayanVeriyiAramaTesti()
    {
        _query.Id = ExperienceTestData.NonexistentId;
        await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_query, CancellationToken.None));
    }
}