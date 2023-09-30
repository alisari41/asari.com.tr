using Application.Tests.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Xunit;
using static asari.com.tr.Application.Features.Experiences.Queries.GetList.GetListExperienceQuery;

namespace Application.Tests.Features.Experiences.Queries.GetList;

public class GetListExperienceTests : ExperienceMockRepository
{
    private readonly GetListExperienceQuery _query;
    private readonly GetListExperienceQueryHandler _handler;

    public GetListExperienceTests(ExperienceFakeData fakeData, GetListExperienceQuery query) : base(fakeData)
    {
        _query = query;
        _handler = new GetListExperienceQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.ToplamVeriCategori)]
    public async Task TumDeneyimVerilerininToplamSayiKarsilastirilmaTesti()
    {
        _query.PageRequest = new PageRequest { Page = 0, PageSize = 15 };
        GetListResponse<GetListExperienceListItemDto> result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: 2, actual: result.Items.Count);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.VeriAramaCategori)]
    public async Task DeneyimAdinaGoreVeriKontrolTesti()
    {
        _query.PageRequest = new PageRequest { Page = 0, PageSize = 15 };
        GetListResponse<GetListExperienceListItemDto> result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: 1, actual: result.Items.Count(item => item.Title == "Software Engineer"));
    }
}