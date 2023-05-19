using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Create;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Delete;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Commands.Update;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetById;
using asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Queries.GetList;
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
        CreateMap<ProgrammingLanguageTechnology, GetListProgrammingLanguageTechnologyListItemDto>()
                                    .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name)) // ProgrammingLanguageName'i map işlemi yapamayacağı için biz verdik
                                                                                                                                    // .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name))     Mesela Başka alanlarıda bu şekilde MAPleyebiliriz
                                    .ReverseMap();
        #endregion

        CreateMap<IPaginate<ProgrammingLanguageTechnology>, GetListResponse<GetListProgrammingLanguageTechnologyListItemDto>>().ReverseMap();

        #endregion

        #region Get By Id
        CreateMap<ProgrammingLanguageTechnology, GetByIdProgrammingLanguageTechnologyGetByIdResponse>()
        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
                                    .ForMember(x => x.ProgrammingLanguageName, opt => opt.MapFrom(x => x.ProgrammingLanguage.Name)) // ProgrammingLanguageName'i map işlemi yapamayacağı için biz verdik
                                    .ForMember(x => x.ProgrammingLanguageId, opt => opt.MapFrom(x => x.ProgrammingLanguage.Id)) // ProgrammingLanguageName'i map işlemi yapamayacağı için biz verdik
                                    .ReverseMap();
        #endregion
        #endregion

        #region Create
        CreateMap<ProgrammingLanguageTechnology, CreatedProgrammingLanguageTechnologyResponse>().ReverseMap();
        CreateMap<ProgrammingLanguageTechnology, CreateProgrammingLanguageTechnologyCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<ProgrammingLanguageTechnology, UpdatedProgrammingLanguageTechnologyResponse>().ReverseMap();
        CreateMap<ProgrammingLanguageTechnology, UpdateProgrammingLanguageTechnologyCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<ProgrammingLanguageTechnology, DeletedProgrammingLanguageTechnologyResponse>().ReverseMap();
        CreateMap<ProgrammingLanguageTechnology, DeleteProgrammingLanguageTechnologyCommand>().ReverseMap();
        #endregion
    }
}