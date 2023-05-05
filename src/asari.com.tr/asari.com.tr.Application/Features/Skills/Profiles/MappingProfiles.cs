using asari.com.tr.Application.Features.Skills.Queries.GetById;
using asari.com.tr.Application.Features.Skills.Commands.Create;
using asari.com.tr.Application.Features.Skills.Commands.Delete;
using asari.com.tr.Application.Features.Skills.Commands.Update;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
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

        #region Get By Id
        CreateMap<Skill, GetByIdSkillGetByIdResponse>().ReverseMap();
        #endregion

        #region Create
        CreateMap<Skill, CreatedSkillResponse>().ReverseMap();
        CreateMap<Skill, CreateSkillCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<Skill, UpdatedSkillResponse>().ReverseMap();
        CreateMap<Skill, UpdateSkillCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<Skill, DeletedSkillResponse>().ReverseMap();
        CreateMap<Skill, DeleteSkillCommand>().ReverseMap();
        #endregion
    }
}