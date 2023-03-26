using asari.com.tr.Application.Features.EducationSkills.Queries.GetList;
using asari.com.tr.Application.Features.EducationSkills.Commands.Create;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;
using asari.com.tr.Application.Features.EducationSkills.Commands.Update;

namespace asari.com.tr.Application.Features.EducationSkills.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region  Get List / İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region Project
        CreateMap<EducationSkill, GetListEducationSkillListItemDto>()
                        .ForMember(x => x.EducationId, opt => opt.MapFrom(x => x.Education.Id))
                        .ForMember(x => x.EducationName, opt => opt.MapFrom(x => x.Education.Name))
        #endregion
        #region Yetenek
                        .ForMember(x => x.SkillId, opt => opt.MapFrom(x => x.Skill.Id))
                        .ForMember(x => x.SkillName, opt => opt.MapFrom(x => x.Skill.Name)).ReverseMap();
        #endregion
        #endregion
        CreateMap<IPaginate<EducationSkill>, GetListResponse<GetListEducationSkillListItemDto>>().ReverseMap();
        #endregion

        #region Create
        CreateMap<EducationSkill, CreatedEducationSkillResponse>().ReverseMap();
        CreateMap<EducationSkill, CreateEducationSkillCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<EducationSkill, UpdatedEducationSkillResponse>().ReverseMap();
        CreateMap<EducationSkill, UpdateEducationSkillCommand>().ReverseMap();
        #endregion
    }
}