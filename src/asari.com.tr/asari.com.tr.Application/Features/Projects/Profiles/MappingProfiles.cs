using asari.com.tr.Application.Features.Projects.Commands.CreateProject;
using asari.com.tr.Application.Features.Projects.Commands.DeleteProject;
using asari.com.tr.Application.Features.Projects.Commands.UpdateProject;
using asari.com.tr.Application.Features.Projects.Dtos;
using asari.com.tr.Application.Features.Projects.Models;
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
        CreateMap<IPaginate<Project>, ProjectListModel>().ReverseMap(); // ProjectListModel sınıfı IPaginate sınıfıyla Maplenir
        CreateMap<Project, ProjectDto>().ReverseMap();
        #endregion

        #region Get By Id
        CreateMap<Project, ProjectGetByIdDto>().ReverseMap();
        #endregion

        #region Create
        CreateMap<Project, CreatedProjectDto>().ReverseMap();
        CreateMap<Project, CreateProjectCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<Project, UpdatedProjectDto>().ReverseMap();
        CreateMap<Project, UpdateProjectCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<Project, DeletedProjectDto>().ReverseMap();
        CreateMap<Project, DeleteProjectCommand>().ReverseMap();
        #endregion
    }
}