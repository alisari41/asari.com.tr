using asari.com.tr.Application.Features.ProjectSkills.Constants;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace asari.com.tr.Application.Features.ProjectSkills.Rules;

public class ProjectSkillBusinessRules : BaseBusinessRules
{
    private readonly IProjectSkillRepository _projectSkillRepository;

    public ProjectSkillBusinessRules(IProjectSkillRepository projectSkillRepository)
    {
        _projectSkillRepository = projectSkillRepository;
    }
    public void ProjectSkillShouldExistWhenRequested(ProjectSkill? projectSkill)
    {
        if (projectSkill == null) throw new BusinessException(ProjectSkillMessages.ProjeYetenegiMevcutDegil);
    }

    public async Task ProjectSkillShouldExistWhenRequested(int id)
    {
        ProjectSkill? result = await _projectSkillRepository.GetAsync(x => x.Id == id, enableTracking: false);
        ProjectSkillShouldExistWhenRequested(result);
    }

    public async Task ProjectSkillSConNotBeDuplicatedWhenInserted(int projectId, int skillId)
    {
        ProjectSkill? result = await _projectSkillRepository.GetAsync(x => (x.ProjectId == projectId) && (x.SkillId == skillId));
        if (result != null) throw new BusinessException(ProjectSkillMessages.ProjeYetenegiMevcut);
    }

    public async Task ProjectSkillTechnologySConNotBeDuplicatedWhenUpdated(ProjectSkill projectSkill)
    {
        ProjectSkill? result = await _projectSkillRepository.GetAsync(x => (x.Id != projectSkill.Id)
                                                                        && (x.ProjectId == projectSkill.ProjectId)
                                                                        && (x.SkillId == projectSkill.SkillId));
        if (result != null) throw new BusinessException(ProjectSkillMessages.ProjeYetenegiMevcut);
    }
}