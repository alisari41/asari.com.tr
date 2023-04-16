using asari.com.tr.Application.Features.Users.Queries.GetList;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace asari.com.tr.Application.Features.Users.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region Get List
        CreateMap<IPaginate<User>, GetListResponse<GetListUserListItemDto>>().ReverseMap();
        CreateMap<User, GetListUserListItemDto>().ReverseMap();
        #endregion
    }
}