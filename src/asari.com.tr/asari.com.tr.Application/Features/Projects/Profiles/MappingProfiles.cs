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
        CreateMap<Project, GetByIdProjectResponse>().ReverseMap();
        #endregion

        #region Create
        CreateMap<Project, CreatedProjectResponse>().ReverseMap();
        CreateMap<Project, CreateProjectCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<Project, UpdatedProjectReponse>().ReverseMap();
        CreateMap<Project, UpdateProjectCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<Project, DeletedProjectResponse>().ReverseMap();
        CreateMap<Project, DeleteProjectCommand>().ReverseMap();
        #endregion
    }
}