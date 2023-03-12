﻿using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Queries.GetList;
using asari.com.tr.Application.Features.TechnologyProjects.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.TechnologyProjects.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        #region  Get List / İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region Teknolojiler
        CreateMap<TechnologyProject, GetListTechnologyProjectListItemDto>()
                        .ForMember(x => x.TechnologyTitle, opt => opt.MapFrom(x => x.Technology.Title))
                        .ForMember(x => x.TechnologyDescription, opt => opt.MapFrom(x => x.Technology.Description))
                        .ForMember(x => x.TechnologyImageUrl, opt => opt.MapFrom(x => x.Technology.ImageUrl))
                        .ForMember(x => x.TechnologyContent, opt => opt.MapFrom(x => x.Technology.Content))

        #endregion
        #region Project
                        .ForMember(x => x.ProjectTitle, opt => opt.MapFrom(x => x.Project.Title))
                        .ForMember(x => x.ProjectDescription, opt => opt.MapFrom(x => x.Project.Description))
                        .ForMember(x => x.ProjectImageUrl, opt => opt.MapFrom(x => x.Project.ImageUrl))
                        .ForMember(x => x.ProjectContent, opt => opt.MapFrom(x => x.Project.Content))
                        .ForMember(x => x.ProjectGithubLink, opt => opt.MapFrom(x => x.Project.GithubLink))
                        .ForMember(x => x.ProjectFolderUrl, opt => opt.MapFrom(x => x.Project.FolderUrl))
                        .ForMember(x => x.ProjectCreateDate, opt => opt.MapFrom(x => x.Project.CreateDate))
        #endregion
        #region Programlama Dili
                        .ForMember(x => x.ProgrammingLanguageDtos, opt => opt.MapFrom(src => GetProgrammingLanguages(src.Project.ProjectProgrammingLanguageTechnologies)))
        #endregion
        #region Programlama Dili Teknolojileri
                        .ForMember(x => x.ProgrammingLanguageTechnologyDtos, opt => opt.MapFrom(src => GetProgrammingLanguageTechnologies(src.Project.ProjectProgrammingLanguageTechnologies))).ReverseMap();
        #endregion
        #endregion
        CreateMap<IPaginate<TechnologyProject>, GetListResponse<GetListTechnologyProjectListItemDto>>().ReverseMap();
        #endregion
    }

    private static List<GetListTechnologyProjectListItemDto.ProgrammingLanguageDto> GetProgrammingLanguages(ICollection<ProjectProgrammingLanguageTechnology> srcProgrammingLanguageTechnologies)
    {
        var getListTechnologyProjectListItemDto = new List<GetListTechnologyProjectListItemDto.ProgrammingLanguageDto>();
        foreach (var item in srcProgrammingLanguageTechnologies)
            getListTechnologyProjectListItemDto.Add(new GetListTechnologyProjectListItemDto.ProgrammingLanguageDto
            {
                ProgrammingLanguageName = item.ProgrammingLanguageTechnology.ProgrammingLanguage.Name
            });

        return getListTechnologyProjectListItemDto;
    }

    private static List<GetListTechnologyProjectListItemDto.ProgrammingLanguageTechnologyDto> GetProgrammingLanguageTechnologies(IEnumerable<ProjectProgrammingLanguageTechnology> srcProgrammingLanguageTechnologies)
    {
        var getListTechnologyProjectListItemDto = new List<GetListTechnologyProjectListItemDto.ProgrammingLanguageTechnologyDto>();
        foreach (var item in srcProgrammingLanguageTechnologies)
            getListTechnologyProjectListItemDto.Add(new GetListTechnologyProjectListItemDto.ProgrammingLanguageTechnologyDto
            {
                ProgrammingLanguageTechnologyName = item.ProgrammingLanguageTechnology.Name
            });

        return getListTechnologyProjectListItemDto;
    }
}
