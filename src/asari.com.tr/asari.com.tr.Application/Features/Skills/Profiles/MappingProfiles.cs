using asari.com.tr.Application.Features.Skills.Queries.GetById;
using asari.com.tr.Application.Features.Skills.Commands.Create;
using asari.com.tr.Application.Features.Skills.Commands.Delete;
using asari.com.tr.Application.Features.Skills.Commands.Update;
using asari.com.tr.Application.Features.Skills.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;
using asari.com.tr.Application.Features.TechnologyProjects.Queries.GetList;

namespace asari.com.tr.Application.Features.Skills.Profiles;

public class MappingProfiles : Profile
{
    // AutoMapper'in Profile sınıfından kalıtım alınır.

    // Mapleme profilleri yazılır

    public MappingProfiles()
    {
        // AutoMapper'in Profile Sınıfından gelir Amacı: Neyi Neye maplicez Source:kaynak Destination: Hedef

        #region Get List

        CreateMap<Skill, GetListSkillListItemDto>()
        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region Proje
                        .ForMember(x => x.ProjectDtos, opt => opt.MapFrom(src => GetListProjects(src.ProjectSkills)))
        #endregion
        #region Lisans ve Sertifikalar
                        .ForMember(x => x.LicenseAndCertificationDtos, opt => opt.MapFrom(src => GetListLicenseAndCertifications(src.LicenseAndCertificationSkills)))
                        .ReverseMap();
        #endregion
        #endregion

        CreateMap<IPaginate<Skill>, GetListResponse<GetListSkillListItemDto>>().ReverseMap();
        #endregion

        #region Get By Id
        CreateMap<Skill, GetByIdSkillResponse>().ReverseMap();
        #endregion

        #region Create
        CreateMap<Skill, CreatedSkillResponse>().ReverseMap();
        CreateMap<Skill, CreateSkillCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<Skill, UpdatedSkillResponse>().ReverseMap();
        CreateMap<Skill, UpdateSkillCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<Skill, DeletedSkillResponse>().ReverseMap();
        CreateMap<Skill, DeleteSkillCommand>().ReverseMap();
        #endregion
    }
    #region Get List - ICollection Mapleme
    private static List<GetListSkillListItemDto.ProjectDto> GetListProjects(ICollection<ProjectSkill> srcProjectSkills)
    {
        var getListProjectListItemDto = new List<GetListSkillListItemDto.ProjectDto>();
        foreach (var item in srcProjectSkills)
            getListProjectListItemDto.Add(new GetListSkillListItemDto.ProjectDto
            {
                ProjectId = item.Project.Id,
                ProjectTitle = item.Project.Title
            });

        return getListProjectListItemDto;
    }

    private static List<GetListSkillListItemDto.LicenseAndCertificationDto> GetListLicenseAndCertifications(IEnumerable<LicenseAndCertificationSkill> srcLicenseAndCertificationSkills)
    {
        var getListLicenseAndCertificationListItemDto = new List<GetListSkillListItemDto.LicenseAndCertificationDto>();
        foreach (var item in srcLicenseAndCertificationSkills)
            getListLicenseAndCertificationListItemDto.Add(new GetListSkillListItemDto.LicenseAndCertificationDto
            {
                LicenseAndCertificationId = item.LicenseAndCertification.Id,
                LicenseAndCertificationName = item.LicenseAndCertification.Name,
                LicenseAndCertificationUrl= item.LicenseAndCertification.CredentialUrl

            });

        return getListLicenseAndCertificationListItemDto;
    }
    #endregion
}