using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using asari.com.tr.Application.Features.Projects.Commands.Create;
using asari.com.tr.Application.Features.Projects.Commands.Delete;
using asari.com.tr.Application.Features.Projects.Commands.Update;
using asari.com.tr.Application.Features.Projects.Queries.GetById;
using asari.com.tr.Application.Features.Projects.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.Projects.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        // AutoMapper'in Profile Sınıfından gelir Amacı: Neyi Neye maplicez Source:kaynak Destination: Hedef

        #region Get List
        CreateMap<IPaginate<Project>, GetListResponse<GetListProjectListItemDto>>().ReverseMap(); // ProjectListModel sınıfı IPaginate sınıfıyla Maplenir
        CreateMap<Project, GetListProjectListItemDto>().ReverseMap();
        #endregion

        #region Get By Id
        CreateMap<Project, GetByIdProjectResponse>()
        #region Teknoloji
                        .ForMember(x => x.TechnologyDtos, opt => opt.MapFrom(src => GetListTechnologies(src.TecgnologyProjects)))
        #endregion
        #region Skill
                        .ForMember(x => x.SkillDtos, opt => opt.MapFrom(src => GetListSkills(src.ProjectSkills)))
                        .ReverseMap();
        #endregion
        #endregion

        #region Create
        CreateMap<Project, CreatedProjectResponse>().ReverseMap();
        CreateMap<Project, CreateProjectCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<Project, UpdatedProjectResponse>().ReverseMap();
        CreateMap<Project, UpdateProjectCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<Project, DeletedProjectResponse>().ReverseMap();
        CreateMap<Project, DeleteProjectCommand>().ReverseMap();
        #endregion
    }

    #region Get List - ICollection Mapleme
    private static List<GetByIdProjectResponse.SkillDto> GetListSkills(ICollection<ProjectSkill> srcProjectSkills)
    {
        var getListSkillListItemDto = new List<GetByIdProjectResponse.SkillDto>();
        foreach (var item in srcProjectSkills)
            getListSkillListItemDto.Add(new GetByIdProjectResponse.SkillDto
            {
                SkillId = item.Skill.Id,
                SkillName = item.Skill.Name
            });

        return getListSkillListItemDto;
    }

    private static List<GetByIdProjectResponse.TechnologyDto> GetListTechnologies(ICollection<TechnologyProject> srcProjectTechnologies)
    {
        var getListTechnologyListItemDto = new List<GetByIdProjectResponse.TechnologyDto>();
        foreach (var item in srcProjectTechnologies)
            getListTechnologyListItemDto.Add(new GetByIdProjectResponse.TechnologyDto
            {
                TechnologyId = item.Technology.Id,
                TechnologyTitle = item.Technology.Title
            });

        return getListTechnologyListItemDto;
    }
    #endregion
}