﻿using asari.com.tr.Application.Features.ExperienceSkills.Constants;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace asari.com.tr.Application.Features.ExperienceSkills.Rules;

public class ExperienceSkillBusinessRules : BaseBusinessRules
{
    private readonly IExperienceSkillRepository _experienceSkillRepository;

    public ExperienceSkillBusinessRules(IExperienceSkillRepository experienceSkillRepository)
    {
        _experienceSkillRepository = experienceSkillRepository;
    }
    public void ExperienceSkillShouldExistWhenRequested(ExperienceSkill? experienceSkill)
    {
        if (experienceSkill == null) throw new BusinessException(ExperienceSkillMessages.DeneyimYetenegiMevcutDegil);
    }

    public async Task ExperienceSkillShouldExistWhenRequested(int id)
    {
        ExperienceSkill? result = await _experienceSkillRepository.GetAsync(x => x.Id == id, enableTracking: false);
        ExperienceSkillShouldExistWhenRequested(result);
    }

    public async Task ExperienceSkillConNotBeDuplicatedWhenInserted(int experienceId, int skillId)
    {
        ExperienceSkill? result = await _experienceSkillRepository.GetAsync(x => (x.ExperienceId == experienceId) && (x.SkillId == skillId));
        if (result != null) throw new BusinessException(ExperienceSkillMessages.DeneyimYetenegiMevcut);
    }

    public async Task ExperienceSkillConNotBeDuplicatedWhenUpdated(ExperienceSkill experienceSkill)
    {
        ExperienceSkill? result = await _experienceSkillRepository.GetAsync(x => (x.Id != experienceSkill.Id)
                                                                        && (x.ExperienceId == experienceSkill.ExperienceId)
                                                                        && (x.SkillId == experienceSkill.SkillId));
        if (result != null) throw new BusinessException(ExperienceSkillMessages.DeneyimYetenegiMevcut);
    }
}