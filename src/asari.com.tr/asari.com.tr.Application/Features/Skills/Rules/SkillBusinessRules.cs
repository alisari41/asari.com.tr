using asari.com.tr.Application.Features.Skills.Constants;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;

namespace asari.com.tr.Application.Features.Skills.Rules;

public class SkillBusinessRules : BaseBusinessRules
{
    private readonly ISkillRepository _skillRepository;

    public SkillBusinessRules(ISkillRepository skillRepository)
    {
        _skillRepository = skillRepository;
    }

    public void SkillShouldExistWhenRequested(Skill? skill)
    {
        if (skill == null) throw new BusinessException(SkillMessages.YetenekMevcutDegil);
    }

    public async Task SkillShouldExistWhenRequested(int id)
    {
        Skill? result = await _skillRepository.GetAsync(x => x.Id == id, enableTracking: false);
        SkillShouldExistWhenRequested(result);
    }

    public async Task SkillTitleConNotBeDuplicatedWhenInserted(string name)
    {
        Skill? result = await _skillRepository.GetAsync(x => string.Equals(x.Name.ToLower(), name.ToLower())); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException(SkillMessages.YetenekMevcut);
    }

    public async Task SkillTitleConNotBeDuplicatedWhenUpdated(Skill skill)
    {
        Skill? result = await _skillRepository.GetAsync(x => (x.Id != skill.Id) && (string.Equals(x.Name.ToLower(), skill.Name.ToLower()))); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException(SkillMessages.YetenekMevcut);
    }
}