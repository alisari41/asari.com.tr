using asari.com.tr.Application.Features.Experiences.Queries.GetById;
using asari.com.tr.Application.Features.Experiences.Commands.Create;
using asari.com.tr.Application.Features.Experiences.Commands.Delete;
using asari.com.tr.Application.Features.Experiences.Commands.Update;
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

        #region Get By Id
        CreateMap<Experience, GetByIdExperienceResponse>().ReverseMap();
        #endregion

        #region Create
        CreateMap<Experience, CreatedExperienceResponse>().ReverseMap();
        CreateMap<Experience, CreateExperienceCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<Experience, UpdatedExperienceResponse>().ReverseMap();
        CreateMap<Experience, UpdateExperienceCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<Experience, DeletedExperienceResponse>().ReverseMap();
        CreateMap<Experience, DeleteExperienceCommand>().ReverseMap();
        #endregion
    }
}