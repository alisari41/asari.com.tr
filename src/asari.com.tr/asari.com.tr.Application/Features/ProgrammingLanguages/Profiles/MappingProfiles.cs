using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Create;
using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Delete;
using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.Update;
using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetById;
using asari.com.tr.Application.Features.ProgrammingLanguages.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        // AutoMapper'in Profile Sınıfından gelir Amacı: Neyi Neye maplicez Source:kaynak Destination: Hedef


        #region Get List
        CreateMap<IPaginate<ProgrammingLanguage>, GetListResponse<GetListProgrammingLanguageListItemDto>>().ReverseMap(); // ProgrammingLanguageListModel sınıfı IPaginate sınıfıyla Maplenir
        CreateMap<ProgrammingLanguage, GetListProgrammingLanguageListItemDto>().ReverseMap();
        #endregion

        #region Get By Id
        CreateMap<ProgrammingLanguage, GetByIdProgrammingLanguageGetByIdResponse>().ReverseMap();
        #endregion

        #region Create
        CreateMap<ProgrammingLanguage, CreatedProgrammingLanguageResponse>().ReverseMap();
        CreateMap<ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<ProgrammingLanguage, UpdatedProgrammingLanguageResponse>().ReverseMap();
        CreateMap<ProgrammingLanguage, UpdateProgrammingLanguageCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<ProgrammingLanguage, DeletedProgrammingLanguageResponse>().ReverseMap();
        CreateMap<ProgrammingLanguage, DeleteProgrammingLanguageCommand>().ReverseMap();
        #endregion
    }
}
