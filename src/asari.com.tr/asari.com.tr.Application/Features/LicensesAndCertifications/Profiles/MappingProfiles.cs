using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Create;
using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Delete;
using asari.com.tr.Application.Features.LicensesAndCertifications.Commands.Update;
using asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetList;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Persistence.Paging;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region Get List
        CreateMap<IPaginate<LicenseAndCertification>, GetListResponse<GetListLicenseAndCertificationListItemDto>>().ReverseMap();
        CreateMap<LicenseAndCertification, GetListLicenseAndCertificationListItemDto>().ReverseMap();
        #endregion

        #region Create
        CreateMap<LicenseAndCertification, CreatedLicenseAndCertificationResponse>().ReverseMap();
        CreateMap<LicenseAndCertification, CreateLicenseAndCertificationCommand>().ReverseMap();
        #endregion

        #region Update
        CreateMap<LicenseAndCertification, UpdatedLicenseAndCertificationResponse>().ReverseMap();
        CreateMap<LicenseAndCertification, UpdateLicenseAndCertificationCommand>().ReverseMap();
        #endregion

        #region Delete
        CreateMap<LicenseAndCertification, DeletedLicenseAndCertificationResponse>().ReverseMap();
        CreateMap<LicenseAndCertification, DeleteLicenseAndCertificationCommand>().ReverseMap();
        #endregion
    }
}