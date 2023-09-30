using Application.Tests.Constants;
using Application.Tests.Mocks.FakeData;
using Application.Tests.Mocks.Repositories;
using asari.com.tr.Application.Features.Educations.Queries.GetList;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Xunit;
using static asari.com.tr.Application.Features.Educations.Queries.GetList.GetListEducationQuery;

namespace Application.Tests.Features.Educations.Queries.GetList;

public class GetListEducationTests : EducationMockRepository
{
    private readonly GetListEducationQuery _getListEducationQuery;
    private readonly GetListEducationQueryHandler _getListEducationQueryHandler;

    public GetListEducationTests(EducationFakeData educationFakeData, GetListEducationQuery getListEducationQuery) : base(educationFakeData)
    {
        _getListEducationQuery = getListEducationQuery;
        _getListEducationQueryHandler = new GetListEducationQueryHandler(MockRepository.Object, Mapper);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.ToplamVeriCategori)]
    public async Task TumEgitimVerilerininToplamSayiKarsilastirilmaTesti()
    {
        _getListEducationQuery.PageRequest = new PageRequest { Page = 0, PageSize = 15 };
        GetListResponse<GetListEducationListItemDto> result = await _getListEducationQueryHandler.Handle(_getListEducationQuery, CancellationToken.None);
        Assert.Equal(expected: 2, actual: result.Items.Count);
    }

    [Fact]
    [Trait(TestCategories.BusinessRulesCategori, TestCategories.VeriAramaCategori)]
    public async Task EgitimAdinaGoreVeriKontrolTesti()
    {
        _getListEducationQuery.PageRequest = new PageRequest { Page = 0, PageSize = 15 };
        GetListResponse<GetListEducationListItemDto> result = await _getListEducationQueryHandler.Handle(_getListEducationQuery, CancellationToken.None);
        Assert.Equal(expected: 1, actual: result.Items.Count(item => item.Name == "Düzce Üniversitesi"));
    }
}