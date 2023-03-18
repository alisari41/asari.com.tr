﻿using asari.com.tr.Application.Features.Educations.Commands.Create;
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

        #region Create
        CreateMap<Education, CreatedEducationResponse>().ReverseMap();
        CreateMap<Education, CreateEducationCommand>().ReverseMap();
        #endregion
    }
}