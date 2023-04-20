using asari.com.tr.Application.Features.LicenseAndCertificationSkills.Constants;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace asari.com.tr.Application.Features.LicenseAndCertificationSkills.Rules;

public class LicenseAndCertificationSkillBusinessRules : BaseBusinessRules
{
    private readonly ILicenseAndCertificationSkillRepository _licenseAndCertificationSkillRepository;

    public LicenseAndCertificationSkillBusinessRules(ILicenseAndCertificationSkillRepository licenseAndCertificationSkillRepository)
    {
        _licenseAndCertificationSkillRepository = licenseAndCertificationSkillRepository;
    }

    public void LicenseAndCertificationSkillShouldExistWhenRequested(LicenseAndCertificationSkill? LicenseAndCertificationSkill)
    {
        if (LicenseAndCertificationSkill == null) throw new BusinessException(LicenseAndCertificationSkillMessages.LisansVeSertifikaYetenegiMevcutDegil);
    }

    public async Task LicenseAndCertificationSkillShouldExistWhenRequested(int id)
    {
        LicenseAndCertificationSkill? result = await _licenseAndCertificationSkillRepository.GetAsync(x => x.Id == id, enableTracking: false);
        LicenseAndCertificationSkillShouldExistWhenRequested(result);
    }

    public async Task LicenseAndCertificationSkillConNotBeDuplicatedWhenInserted(int licenseAndCertificationId, int skillId)
    {
        LicenseAndCertificationSkill? result = await _licenseAndCertificationSkillRepository.GetAsync(x => (x.LicenseAndCertificationId == licenseAndCertificationId) && (x.SkillId == skillId));
        if (result != null) throw new BusinessException(LicenseAndCertificationSkillMessages.LisansVeSertifikaYetenegiMevcut);
    }

    public async Task LicenseAndCertificationSkillConNotBeDuplicatedWhenUpdated(LicenseAndCertificationSkill licenseAndCertificationSkill)
    {
        LicenseAndCertificationSkill? result = await _licenseAndCertificationSkillRepository.GetAsync(x => (x.Id != licenseAndCertificationSkill.Id)
                                                                        && (x.LicenseAndCertificationId == licenseAndCertificationSkill.LicenseAndCertificationId)
                                                                        && (x.SkillId == licenseAndCertificationSkill.SkillId));
        if (result != null) throw new BusinessException(LicenseAndCertificationSkillMessages.LisansVeSertifikaYetenegiMevcut);
    }
}