using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using asari.com.tr.Application.Features.ProgrammingLanguages.Commands.UpdateProgrammingLanguage;
using asari.com.tr.Application.Features.ProgrammingLanguages.Dtos;
using asari.com.tr.Application.Features.ProgrammingLanguages.Models;
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
        CreateMap<IPaginate<ProgrammingLanguage>, ProgrammingLanguageListModel>().ReverseMap(); // ProgrammingLanguageListModel sınıfı IPaginate sınıfıyla Maplenir
        CreateMap<ProgrammingLanguage, ProgrammingLanguageDto>().ReverseMap();
        #endregion

        #region Get By Id
        CreateMap<ProgrammingLanguage, ProgrammingLanguageGetByIdDto>().ReverseMap();
        #endregion

        #region Create
        CreateMap<ProgrammingLanguage, CreatedProgrammingLanguageDto>().ReverseMap();
        CreateMap<ProgrammingLanguage, CreateProgrammingLanguageCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<ProgrammingLanguage, UpdatedProgrammingLanguageDto>().ReverseMap();
        CreateMap<ProgrammingLanguage, UpdateProgrammingLanguageCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<ProgrammingLanguage, DeletedProgrammingLanguageDto>().ReverseMap();
        CreateMap<ProgrammingLanguage, DeleteProgrammingLanguageCommand>().ReverseMap();
        #endregion
    }
}
