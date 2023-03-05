using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Create;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Delete;
using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Commands.Update;
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
                CreateMap<ProjectProgrammingLanguageTechnology, GetListProjectProgrammingLanguageTechnologyListDto>()
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
        CreateMap<IPaginate<ProjectProgrammingLanguageTechnology>, GetListResponse<GetListProjectProgrammingLanguageTechnologyListDto>>().ReverseMap();
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
}