using asari.com.tr.Application.Features.ExperienceSkills.Commands.Create;
using asari.com.tr.Application.Features.ExperienceSkills.Commands.Update;
using asari.com.tr.Application.Features.ExperienceSkills.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.ExperienceSkills.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region  Get List / İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region Project
        CreateMap<ExperienceSkill, GetListExperienceSkillListItemDto>()
                        .ForMember(x => x.ExperienceId, opt => opt.MapFrom(x => x.Experience.Id))
                        .ForMember(x => x.ExperienceTitle, opt => opt.MapFrom(x => x.Experience.Title))
        #endregion
        #region Yetenek
                        .ForMember(x => x.SkillId, opt => opt.MapFrom(x => x.Skill.Id))
                        .ForMember(x => x.SkillName, opt => opt.MapFrom(x => x.Skill.Name)).ReverseMap();
        #endregion
        #endregion
        CreateMap<IPaginate<ExperienceSkill>, GetListResponse<GetListExperienceSkillListItemDto>>().ReverseMap();
        #endregion

        #region Create
        CreateMap<ExperienceSkill, CreatedExperienceSkillResponse>().ReverseMap();
        CreateMap<ExperienceSkill, CreateExperienceSkillCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<ExperienceSkill, UpdatedExperienceSkillResponse>().ReverseMap();
        CreateMap<ExperienceSkill, UpdateExperienceSkillCommand>().ReverseMap();
        #endregion
    }
}