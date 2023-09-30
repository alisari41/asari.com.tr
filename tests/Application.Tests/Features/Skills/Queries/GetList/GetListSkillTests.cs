using Application.Tests.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Xunit;
using static asari.com.tr.Application.Features.Skills.Queries.GetList.GetListSkillQuery;

namespace Application.Tests.Features.Skills.Queries.GetList;

public class GetListSkillTests : SkillMockRepository
{
    private readonly GetListSkillQuery _query;
    private readonly GetListSkillQueryHandler _handler;

    public GetListSkillTests(SkillFakeData fakeData, GetListSkillQuery query) : base(fakeData)
    {
        _query = query;
        _handler = new GetListSkillQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.ToplamVeriCategori)]
    public async Task TumYetenekVerilerininToplamSayiKarsilastirilmaTesti()
    {
        _query.PageRequest = new PageRequest { Page = 0, PageSize = 15 };
        GetListResponse<GetListSkillListItemDto> result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: 2, actual: result.Items.Count);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.VeriAramaCategori)]
    public async Task YetenekAdinaGoreVeriKontrolTesti()
    {
        _query.PageRequest = new PageRequest { Page = 0, PageSize = 15 };
        GetListResponse<GetListSkillListItemDto> result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: 1, actual: result.Items.Count(item => item.Name == "C#"));
    }
}