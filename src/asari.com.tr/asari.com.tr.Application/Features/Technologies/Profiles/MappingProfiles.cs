using asari.com.tr.Application.Features.Technologies.Commands.Create;
using asari.com.tr.Application.Features.Technologies.Queries.GetById;
using asari.com.tr.Application.Features.Technologies.Queries.GetList;
using asari.com.tr.Application.Features.Technologies.Queries.GetListByDynamic;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.Technologies.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        // AutoMapper'in Profile Sınıfından gelir Amacı: Neyi Neye maplicez Source:kaynak Destination: Hedef

        #region Get List
        CreateMap<IPaginate<Technology>, GetListResponse<GetListTechnologyListItemDto>>().ReverseMap();
        CreateMap<Technology, GetListTechnologyListItemDto>().ReverseMap();
        #endregion

        #region Get By Id
        CreateMap<Technology, GetByIdTechnologyResponse>().ReverseMap();
        #endregion

        #region Get List By Dynamic
        CreateMap<IPaginate<Technology>, GetListResponse<GetListByDynamicTechnologyListItemDto>>().ReverseMap();
        CreateMap<Technology, GetListByDynamicTechnologyListItemDto>().ReverseMap();
        #endregion

        #region Create
        CreateMap<Technology, CreatedTechnologyResponse>().ReverseMap();
        CreateMap<Technology, CreateTechnologyCommand>().ReverseMap();
        #endregion
    }
}