using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Rules;

public class LicenseAndCertificationBusinessRules : BaseBusinessRules
{
    private readonly ILicenseAndCertificationRepository _licenseAndCertificationRepository;

    public LicenseAndCertificationBusinessRules(ILicenseAndCertificationRepository licenseAndCertificationRepository)
    {
        _licenseAndCertificationRepository = licenseAndCertificationRepository;
    }

    public void LicenseAndCertificationShouldExistWhenRequested(LicenseAndCertification? licenseAndCertification)
    {
        if (licenseAndCertification == null) throw new BusinessException("Lisanslar ve Sertifikalar mevcut değildir.");
    }

    public async Task LicenseAndCertificationShouldExistWhenRequested(int id)
    {
        LicenseAndCertification? result = await _licenseAndCertificationRepository.GetAsync(x => x.Id == id, enableTracking: false);
        LicenseAndCertificationShouldExistWhenRequested(result);
    }

    public async Task LicenseAndCertificationTitleConNotBeDuplicatedWhenInserted(string name)
    {
        LicenseAndCertification? result = await _licenseAndCertificationRepository.GetAsync(x => string.Equals(x.Name.ToLower(), name.ToLower())); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException("Lisanslar ve Sertifikalar Başlığı kullanılmaktadır!");
    }

    public async Task LicenseAndCertificationTitleConNotBeDuplicatedWhenUpdated(LicenseAndCertification licenseAndCertification)
    {
        LicenseAndCertification? result = await _licenseAndCertificationRepository.GetAsync(x => (x.Id != licenseAndCertification.Id) && string.Equals(x.Name.ToLower(), licenseAndCertification.Name.ToLower())); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException("Lisanslar ve Sertifikalar Başlığı kullanılmaktadır!");
    }
}