using asari.com.tr.Application.Features.UserOperationClaims.Queries.GetList;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace asari.com.tr.Application.Features.UserOperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region  Get List / İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region Kullanıcı
        CreateMap<UserOperationClaim, GetListUserOperationClaimListItemDto>()
                        .ForMember(x => x.UserId, opt => opt.MapFrom(x => x.User.Id))
                        .ForMember(x => x.UserFirstName, opt => opt.MapFrom(x => x.User.FirstName))
                        .ForMember(x => x.UserLastName, opt => opt.MapFrom(x => x.User.LastName))
                        .ForMember(x => x.UserEmail, opt => opt.MapFrom(x => x.User.Email))
        #endregion
        #region Rol
                        .ForMember(x => x.OperationClaimId, opt => opt.MapFrom(x => x.OperationClaim.Id))
                        .ForMember(x => x.OperationClaimName, opt => opt.MapFrom(x => x.OperationClaim.Name)).ReverseMap();
        #endregion
        #endregion
        CreateMap<IPaginate<UserOperationClaim>, GetListResponse<GetListUserOperationClaimListItemDto>>().ReverseMap();
        #endregion
    }
}