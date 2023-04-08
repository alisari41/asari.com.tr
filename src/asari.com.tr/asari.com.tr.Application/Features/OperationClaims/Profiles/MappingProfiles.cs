using asari.com.tr.Application.Features.OperationClaims.Commands.Create;
using asari.com.tr.Application.Features.OperationClaims.Commands.Update;
using asari.com.tr.Application.Features.OperationClaims.Queries.GetList;
using AutoMapper;
using Core.Persistence.Paging;
using Core.Security.Entities;

namespace asari.com.tr.Application.Features.OperationClaims.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region Get List
        CreateMap<IPaginate<OperationClaim>, GetListResponse<OperationClaimListItemDto>>().ReverseMap(); // OperationClaimListModel sınıfı IPaginate sınıfıyla Maplenir
        CreateMap<OperationClaim, OperationClaimListItemDto>().ReverseMap();
        #endregion

        #region Create
        CreateMap<OperationClaim, CreatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, CreateOperationClaimCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<OperationClaim, UpdatedOperationClaimResponse>().ReverseMap();
        CreateMap<OperationClaim, UpdateOperationClaimCommand>().ReverseMap();
        #endregion
    }
}