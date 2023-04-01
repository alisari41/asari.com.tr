using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region  Get List / İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region İlişkili Tabloda Mapleme işlemi gerçekleştirmesi
        #region Lisans ve Sertifika
        CreateMap<LicenseAndCertificationSkill, GetListLicenseAndCertificationSkillListItemDto>()
                        .ForMember(x => x.LicenseAndCertificationId, opt => opt.MapFrom(x => x.LicenseAndCertification.Id))
                        .ForMember(x => x.LicenseAndCertificationName, opt => opt.MapFrom(x => x.LicenseAndCertification.Name))
        #endregion
        #region Yetenek
                        .ForMember(x => x.SkillId, opt => opt.MapFrom(x => x.Skill.Id))
                        .ForMember(x => x.SkillName, opt => opt.MapFrom(x => x.Skill.Name)).ReverseMap();
        #endregion
        #endregion
        CreateMap<IPaginate<LicenseAndCertificationSkill>, GetListResponse<GetListLicenseAndCertificationSkillListItemDto>>().ReverseMap();
        #endregion
    }
}