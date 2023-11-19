using asari.com.tr.Application.Features.EducationSkills.Constants;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace asari.com.tr.Application.Features.EducationSkills.Rules;

public class EducationSkillBusinessRules : BaseBusinessRules
{
    private readonly IEducationSkillRepository _educationSkillRepository;

    public EducationSkillBusinessRules(IEducationSkillRepository educationSkillRepository)
    {
        _educationSkillRepository = educationSkillRepository;
    }

    public void EducationSkillShouldExistWhenRequested(EducationSkill? EducationSkill)
    {
        if (EducationSkill == null) throw new BusinessException(EducationSkillMessages.EgitimYetenegiMevcutDegil);
    }

    public async Task EducationSkillShouldExistWhenRequested(int id)
    {
        EducationSkill? result = await _educationSkillRepository.GetAsync(x => x.Id == id, enableTracking: false);
        EducationSkillShouldExistWhenRequested(result);
    }

    public async Task EducationSkillConNotBeDuplicatedWhenInserted(int? educationId, int? skillId)
    {
        EducationSkill? result = await _educationSkillRepository.GetAsync(x => (x.EducationId == educationId) && (x.SkillId == skillId));
        if (result != null) throw new BusinessException(EducationSkillMessages.EgitimYetenegiMevcut);
    }

    public async Task EducationSkillConNotBeDuplicatedWhenUpdated(EducationSkill educationSkill)
    {
        EducationSkill? result = await _educationSkillRepository.GetAsync(x => (x.Id != educationSkill.Id)
                                                                        && (x.EducationId == educationSkill.EducationId)
                                                                        && (x.SkillId == educationSkill.SkillId));
        if (result != null) throw new BusinessException(EducationSkillMessages.EgitimYetenegiMevcut);
    }
}