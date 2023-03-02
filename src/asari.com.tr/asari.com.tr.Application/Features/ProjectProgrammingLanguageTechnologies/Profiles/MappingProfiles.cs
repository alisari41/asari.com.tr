using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.CreateProjectProgrammingLanguageTechnology;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.UpdateProjectProgrammingLanguageTechnology;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Dtos;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Models;
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
                CreateMap<ProjectProgrammingLanguageTechnology, ProjectProgrammingLanguageTechnologyListDto>()
                                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Project.Title))
                                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Project.Description))
                                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Project.ImageUrl))
                                .ForMember(x => x.Content, opt => opt.MapFrom(x => x.Project.Content))
                                .ForMember(x => x.GithubLink, opt => opt.MapFrom(x => x.Project.GithubLink))
                                .ForMember(x => x.FolderUrl, opt => opt.MapFrom(x => x.Project.FolderUrl))
                                .ForMember(x => x.CreateDate, opt => opt.MapFrom(x => x.Project.CreateDate))
                #endregion
                #region Programlama Dili
                                .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguageTechnology.ProgrammingLanguage.Name))
                #endregion
                #region Programlama Dili Teknolojileri
                                .ForMember(x => x.ProgrammingLanguageTechnologyName, opt => opt.MapFrom(x => x.ProgrammingLanguageTechnology.Name)).ReverseMap();
                #endregion
            #endregion
        CreateMap<IPaginate<ProjectProgrammingLanguageTechnology>, ProjectProgrammingLanguageTechnologyListModel>().ReverseMap();
        #endregion

        #region Create
        CreateMap<ProjectProgrammingLanguageTechnology, CreatedProjectProgrammingLanguageTechnologyDto>().ReverseMap();
        CreateMap<ProjectProgrammingLanguageTechnology, CreateProjectProgrammingLanguageTechnologyCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<ProjectProgrammingLanguageTechnology, UpdatedProjectProgrammingLanguageTechnologyDto>().ReverseMap();
        CreateMap<ProjectProgrammingLanguageTechnology, UpdateProjectProgrammingLanguageTechnologyCommand>().ReverseMap(); 
        #endregion
    }
}