using Application.Tests.Constants;
using Application.Tests.Mocks.FakeData;
using asari.com.tr.Application.Features.EducationSkills.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using static asari.com.tr.Application.Features.EducationSkills.Queries.GetList.GetListEducationSkillQuery;
using Xunit;
using Application.Tests.Mocks.Repositories;
using Application.Tests.Features.EducationSkills.Constants;

namespace Application.Tests.Features.EducationSkills.Queries.GetList;

public class GetListEducationSkillTests : EducationSkillMockRepository
{
    private readonly GetListEducationSkillQuery _query;
    private readonly GetListEducationSkillQueryHandler _handler;

    public GetListEducationSkillTests(EducationSkillFakeData fakeData, GetListEducationSkillQuery query) : base(fakeData)
    {
        _query = query;
        _handler = new GetListEducationSkillQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.ToplamVeriCategori)]
    public async Task TumEducationSkillVerilerininToplamSayiKarsilastirilmaTesti()
    {
        _query.PageRequest = new PageRequest { Page = 0, PageSize = 15 };
        GetListResponse<GetListEducationSkillListItemDto> result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: 2, actual: result.Items.Count);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.VeriAramaCategori)]
    public async Task EducationSkillAdinaGoreVeriKontrolTesti()
    {
        _query.PageRequest = new PageRequest { Page = 0, PageSize = 15 };
        GetListResponse<GetListEducationSkillListItemDto> result = await _handler.Handle(_query, CancellationToken.None);
        Assert.Equal(expected: 1, actual: result.Items.Count(item => item.Id == EducationSkillTestData.UpdateId));
    }
}