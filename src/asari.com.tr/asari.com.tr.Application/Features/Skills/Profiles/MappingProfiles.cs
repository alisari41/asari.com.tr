﻿using asari.com.tr.Application.Features.Skills.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.Skills.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        // AutoMapper'in Profile Sınıfından gelir Amacı: Neyi Neye maplicez Source:kaynak Destination: Hedef

        #region Get List
        CreateMap<IPaginate<Skill>, GetListResponse<GetListSkillListItemDto>>().ReverseMap();
        CreateMap<Skill, GetListSkillListItemDto>().ReverseMap();
        #endregion
    }

}