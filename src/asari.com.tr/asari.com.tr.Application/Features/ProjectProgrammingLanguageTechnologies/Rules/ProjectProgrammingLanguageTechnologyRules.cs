using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Rules;

public class ProjectProgrammingLanguageTechnologyRules
{
    private readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;

    public ProjectProgrammingLanguageTechnologyRules(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository)
    {
        _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
    }

    public void ProjectProgrammingLanguageTechnologyShouldExistWhenRequested(ProjectProgrammingLanguageTechnology? projectProgrammingLanguageTechnology)
    {
        if (projectProgrammingLanguageTechnology == null) throw new BusinessException("Proje Programlama dili mevcut değildir.");
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
        if (result != null) throw new BusinessException("Proje Programalama Dili kullanılmaktadır!");
    }

    public async Task ProjectProgrammingLanguageTechnologySConNotBeDuplicatedWhenUpdated(ProjectProgrammingLanguageTechnology projectProgrammingLanguageTechnology)
    {
        ProjectProgrammingLanguageTechnology? result = await _projectProgrammingLanguageTechnologyRepository.GetAsync(x => (x.Id != projectProgrammingLanguageTechnology.Id)
                                                                                                            && (x.ProgrammingLanguageTechnologyId == projectProgrammingLanguageTechnology.ProgrammingLanguageTechnologyId)
                                                                                                            && (x.ProjectId == projectProgrammingLanguageTechnology.ProjectId));
        if (result != null) throw new BusinessException("Proje Programalama Dili kullanılmaktadır!");
    }
}
