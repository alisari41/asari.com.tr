using Application.Tests.Constants;
using Application.Tests.Features.Educations.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Educations.Queries.GetById;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Xunit;
using static asari.com.tr.Application.Features.Educations.Queries.GetById.GetByIdEducationQuery;

namespace Application.Tests.Features.Educations.Queries.GetById;

public class GetByIdEducationTests : EducationMockRepository
{
    private readonly GetByIdEducationQuery _query;
    private readonly GetByIdEducationQueryHandler _handler;

    public GetByIdEducationTests(EducationFakeData fakeData, GetByIdEducationQuery query) : base(fakeData)
    {
        _query = query;
        _handler = new GetByIdEducationQueryHandler(MockRepository.Object, BusinessRules, Mapper);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.VeriAramaCategori)]
    public async Task EducationIdsineGoreAramaTesti()
    {
        _query.Id = EducationTestData.UpdateId;
        GetByIdEducationResponse result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: "Derince Ticaret Meslek", result.Name);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.OlmayanVeriCategori)]
    public async Task EducationTablosundaOlmayanVeriyiAramaTesti()
    {
        _query.Id = EducationTestData.NonexistentId;
        await Assert.ThrowsAsync<BusinessException>(async () => await _handler.Handle(_query, CancellationToken.None));
    }
}