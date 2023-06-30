using asari.com.tr.Application.Features.Experiences.Queries.GetById;
using asari.com.tr.Application.Features.Experiences.Commands.Create;
using asari.com.tr.Application.Features.Experiences.Commands.Delete;
using asari.com.tr.Application.Features.Experiences.Commands.Update;
using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;
using asari.com.tr.Application.Features.Skills.Queries.GetList;

namespace asari.com.tr.Application.Features.Experiences.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region Get List
        CreateMap<Experience, GetListExperienceListItemDto>()
        #region Skill
                        .ForMember(x => x.SkillDtos, opt => opt.MapFrom(src => GetListProjects(src.ExperienceSkills)))
                        .ReverseMap();
        #endregion
        CreateMap<IPaginate<Experience>, GetListResponse<GetListExperienceListItemDto>>().ReverseMap();
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

    #region Get List - ICollection Mapleme
    private static List<GetListExperienceListItemDto.SkillDto> GetListProjects(ICollection<ExperienceSkill> srcExperienceSkills)
    {
        var getListSkillListItemDto = new List<GetListExperienceListItemDto.SkillDto>();
        foreach (var item in srcExperienceSkills)
            getListSkillListItemDto.Add(new GetListExperienceListItemDto.SkillDto
            {
                SkillId = item.Skill.Id,
                SkillName = item.Skill.Name
            });

        return getListSkillListItemDto;
    }
    #endregion
}