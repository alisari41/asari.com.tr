using asari.com.tr.Application.Features.Educations.Commands.Create;
using asari.com.tr.Application.Features.Educations.Commands.Delete;
using asari.com.tr.Application.Features.Educations.Commands.Update;
using asari.com.tr.Application.Features.Educations.Queries.GetById;
using asari.com.tr.Application.Features.Educations.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.Educations.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region Get List
        CreateMap<IPaginate<Education>, GetListResponse<GetListEducationListItemDto>>().ReverseMap();
        CreateMap<Education, GetListEducationListItemDto>().ReverseMap();
        #endregion

        #region Get By Id
        CreateMap<Education, GetByIdEducationResponse>().ReverseMap();
        #endregion

        #region Create
        CreateMap<Education, CreatedEducationResponse>().ReverseMap();
        CreateMap<Education, CreateEducationCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<Education, UpdatedEducationResponse>().ReverseMap();
        CreateMap<Education, UpdateEducationCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<Education, DeletedEducationResponse>().ReverseMap();
        CreateMap<Education, DeleteEducationCommand>().ReverseMap();
        #endregion
    }
}