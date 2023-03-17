using asari.com.tr.Application.Features.ProjectSkills.Commands.Create;
using asari.com.tr.Application.Features.ProjectSkills.Commands.Update;
using asari.com.tr.Application.Features.ProjectSkills.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.ProjectSkills.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region  Get List / İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region Project
        CreateMap<ProjectSkill, GetListProjectSkillListItemDto>()
                        .ForMember(x => x.ProjectId, opt => opt.MapFrom(x => x.Project.Id))
                        .ForMember(x => x.ProjectTitle, opt => opt.MapFrom(x => x.Project.Title))
        #endregion
        #region Yetenek
                        .ForMember(x => x.SkillId, opt => opt.MapFrom(x => x.Skill.Id))
                        .ForMember(x => x.SkillName, opt => opt.MapFrom(x => x.Skill.Name)).ReverseMap();
        #endregion
        #endregion
        CreateMap<IPaginate<ProjectSkill>, GetListResponse<GetListProjectSkillListItemDto>>().ReverseMap();
        #endregion

        #region Create
        CreateMap<ProjectSkill, CreatedProjectSkillResponse>().ReverseMap();
        CreateMap<ProjectSkill, CreateProjectSkillCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<ProjectSkill, UpdatedProjectSkillResponse>().ReverseMap();
        CreateMap<ProjectSkill, UpdateProjectSkillCommand>().ReverseMap();
        #endregion
    }
}