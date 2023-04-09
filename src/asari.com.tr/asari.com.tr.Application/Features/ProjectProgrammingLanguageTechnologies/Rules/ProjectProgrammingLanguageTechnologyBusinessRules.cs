using asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Constants;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Rules;

public class ProjectProgrammingLanguageTechnologyBusinessRules : BaseBusinessRules
{
    private readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;

    public ProjectProgrammingLanguageTechnologyBusinessRules(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository)
    {
        _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
    }

    public void ProjectProgrammingLanguageTechnologyShouldExistWhenRequested(ProjectProgrammingLanguageTechnology? projectProgrammingLanguageTechnology)
    {
        if (projectProgrammingLanguageTechnology == null) throw new BusinessException(ProjectProgrammingLanguageTechnologyMessages.ProjeProgramlamaDiliTeknolojisiMevcutDegil);
    }

    public async Task ProjectProgrammingLanguageTechnologyShouldExistWhenRequested(int id)
    {
        ProjectProgrammingLanguageTechnology? result = await _projectProgrammingLanguageTechnologyRepository.GetAsync(x => x.Id == id, enableTracking: false);
        ProjectProgrammingLanguageTechnologyShouldExistWhenRequested(result);
    }

    public async Task ProjectProgrammingLanguageTechnologySConNotBeDuplicatedWhenInserted(int programmingLanguageTechnologyId, int projectId)
    {
        ProjectProgrammingLanguageTechnology? result = await _projectProgrammingLanguageTechnologyRepository.GetAsync(x => (x.ProgrammingLanguageTechnologyId == programmingLanguageTechnologyId)
                                                                                                                                                && (x.ProjectId == projectId));
        if (result != null) throw new BusinessException(ProjectProgrammingLanguageTechnologyMessages.ProjeProgramlamaDiliTeknolojisiMevcut);
    }

    public async Task ProjectProgrammingLanguageTechnologySConNotBeDuplicatedWhenUpdated(ProjectProgrammingLanguageTechnology projectProgrammingLanguageTechnology)
    {
        ProjectProgrammingLanguageTechnology? result = await _projectProgrammingLanguageTechnologyRepository.GetAsync(x => (x.Id != projectProgrammingLanguageTechnology.Id)
                                                                                                            && (x.ProgrammingLanguageTechnologyId == projectProgrammingLanguageTechnology.ProgrammingLanguageTechnologyId)
                                                                                                            && (x.ProjectId == projectProgrammingLanguageTechnology.ProjectId));
        if (result != null) throw new BusinessException(ProjectProgrammingLanguageTechnologyMessages.ProjeProgramlamaDiliTeknolojisiMevcut);
    }
}