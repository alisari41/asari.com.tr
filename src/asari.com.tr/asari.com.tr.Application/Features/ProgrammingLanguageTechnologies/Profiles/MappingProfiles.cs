using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.CreateProgrammingLanguageTechnology;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Dtos;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Models;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        #region Get List / İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        CreateMap<ProgrammingLanguageTechnology, ProgrammingLanguageTechnologyDto>()
                                    .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name)) // ProgrammingLanguageName'i map işlemi yapamayacağı için biz verdik
                                    // .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name))     Mesela Başka alanlarıda bu şekilde MAPleyebiliriz
                                    .ReverseMap();
        #endregion

        CreateMap<IPaginate<ProgrammingLanguageTechnology>, ProgrammingLanguageTechnologyListModel>().ReverseMap();

        #endregion


        #region Create
        CreateMap<ProgrammingLanguageTechnology, CreatedProgrammingLanguageTechnologyDto>().ReverseMap();
        CreateMap<ProgrammingLanguageTechnology, CreateProgrammingLanguageTechnologyCommand>().ReverseMap();
        #endregion

    }
}
