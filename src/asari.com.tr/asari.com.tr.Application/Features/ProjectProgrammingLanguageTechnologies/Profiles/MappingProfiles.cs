using asari.com.tr.Application.Features.Experiences.Queries.GetList;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Create;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Delete;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Update;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetById;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        #region  Get List / İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
            #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
                #region Project
                CreateMap<ProjectProgrammingLanguageTechnology, GetListProjectProgrammingLanguageTechnologyListItemDto>()
                                .ForMember(x => x.ProjectId, opt => opt.MapFrom(x => x.Project.Id))
                                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Project.Title))
                                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Project.Description))
                                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Project.ImageUrl))
                                .ForMember(x => x.Content, opt => opt.MapFrom(x => x.Project.Content))
                                .ForMember(x => x.GithubLink, opt => opt.MapFrom(x => x.Project.GithubLink))
                                .ForMember(x => x.FolderUrl, opt => opt.MapFrom(x => x.Project.FolderUrl))
                                .ForMember(x => x.CreateDate, opt => opt.MapFrom(x => x.Project.CreateDate))
                #endregion
                #region Programlama Dili
                                .ForMember(x => x.ProgrammingLanguageId, opt => opt.MapFrom(x => x.ProgrammingLanguageTechnology.ProgrammingLanguage.Id))
                                .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguageTechnology.ProgrammingLanguage.Name))
                #endregion
                #region Programlama Dili Teknolojileri
                                .ForMember(x => x.ProgrammingLanguageTechnologyId, opt => opt.MapFrom(x => x.ProgrammingLanguageTechnology.Id))
                                .ForMember(x => x.ProgrammingLanguageTechnologyName, opt => opt.MapFrom(x => x.ProgrammingLanguageTechnology.Name))
                #endregion
                #region Skill - Yenetenekler        
                                .ForMember(x => x.SkillDtos, opt => opt.MapFrom(src => GetListProjects(src.Project.ProjectSkills)))
                                .ReverseMap();
                #endregion
        #endregion
        CreateMap<IPaginate<ProjectProgrammingLanguageTechnology>, GetListResponse<GetListProjectProgrammingLanguageTechnologyListItemDto>>().ReverseMap();
        #endregion

        #region Get By Id
        #region Project
        CreateMap<ProjectProgrammingLanguageTechnology, GetByIdProjectProgrammingLanguageTechnologyResponse>()
                        .ForMember(x => x.ProjectId, opt => opt.MapFrom(x => x.Project.Id))
                        .ForMember(x => x.ProjectTitle, opt => opt.MapFrom(x => x.Project.Title))
                        .ForMember(x => x.ProjectDescription, opt => opt.MapFrom(x => x.Project.Description))
                        .ForMember(x => x.ProjectImageUrl, opt => opt.MapFrom(x => x.Project.ImageUrl))
                        .ForMember(x => x.ProjectContent, opt => opt.MapFrom(x => x.Project.Content))
                        .ForMember(x => x.ProjectGithubLink, opt => opt.MapFrom(x => x.Project.GithubLink))
                        .ForMember(x => x.ProjectFolderUrl, opt => opt.MapFrom(x => x.Project.FolderUrl))
                        .ForMember(x => x.ProjectCreateDate, opt => opt.MapFrom(x => x.Project.CreateDate))
        #endregion
        #region Programlama Dili
                        .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguageTechnology.ProgrammingLanguage.Name))
        #endregion
        #region Programlama Dili Teknolojileri
                        .ForMember(x => x.ProgrammingLanguageTechnologyId, opt => opt.MapFrom(x => x.ProgrammingLanguageTechnology.Id))
                        .ForMember(x => x.ProgrammingLanguageTechnologyName, opt => opt.MapFrom(x => x.ProgrammingLanguageTechnology.Name)).ReverseMap();
        #endregion
        #endregion

        #region Create
        CreateMap<ProjectProgrammingLanguageTechnology, CreatedProjectProgrammingLanguageTechnologyResponse>().ReverseMap();
        CreateMap<ProjectProgrammingLanguageTechnology, CreateProjectProgrammingLanguageTechnologyCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<ProjectProgrammingLanguageTechnology, UpdatedProjectProgrammingLanguageTechnologyResponse>().ReverseMap();
        CreateMap<ProjectProgrammingLanguageTechnology, UpdateProjectProgrammingLanguageTechnologyCommand>().ReverseMap(); 
        #endregion

        #region Delete
        CreateMap<ProjectProgrammingLanguageTechnology, DeletedProjectProgrammingLanguageTechnologyResponse>().ReverseMap();
        CreateMap<ProjectProgrammingLanguageTechnology, DeleteProjectProgrammingLanguageTechnologyCommand>().ReverseMap(); 
        #endregion
    }

    #region Get List - ICollection Mapleme
    private static List<GetListProjectProgrammingLanguageTechnologyListItemDto.SkillDto> GetListProjects(ICollection<ProjectSkill> srcProjectSkills)
    {
        var getListSkillListItemDto = new List<GetListProjectProgrammingLanguageTechnologyListItemDto.SkillDto>();
        foreach (var item in srcProjectSkills)
            getListSkillListItemDto.Add(new GetListProjectProgrammingLanguageTechnologyListItemDto.SkillDto
            {
                SkillId = item.Skill.Id,
                SkillName = item.Skill.Name
            });

        return getListSkillListItemDto;
    }
    #endregion
}