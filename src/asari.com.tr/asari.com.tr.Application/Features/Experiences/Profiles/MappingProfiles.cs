using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.Experiences.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region Get List
        CreateMap<IPaginate<Experience>, GetListResponse<GetListExperienceListItemDto>>().ReverseMap();
        CreateMap<Experience, GetListExperienceListItemDto>().ReverseMap();
        #endregion
    }
}