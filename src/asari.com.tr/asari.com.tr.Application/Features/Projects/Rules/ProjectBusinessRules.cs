using asari.com.tr.Application.Features.Projects.Constants;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace asari.com.tr.Application.Features.Projects.Rules;

public class ProjectBusinessRules : BaseBusinessRules
{
    private readonly IProjectRepository _projectRepository;

    public ProjectBusinessRules(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public void ProgrammingLanguageShouldExistWhenRequested(Project? project)
    {
        if (project == null) throw new BusinessException(ProjectMessages.ProjeMevcutDegil);
    }

    public async Task ProjectShouldExistWhenRequested(int id)
    {
        Project? result = await _projectRepository.GetAsync(x => x.Id == id, enableTracking: false);
        ProgrammingLanguageShouldExistWhenRequested(result);
    }

    public async Task ProjectTitleConNotBeDuplicatedWhenInserted(string title)
    {
        Project? result = await _projectRepository.GetAsync(x => string.Equals(x.Title.ToLower(), title.ToLower())); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException(ProjectMessages.ProjeMevcut);
    }

    public async Task ProjectTitleConNotBeDuplicatedWhenUpdated(Project project)
    {
        Project? result = await _projectRepository.GetAsync(x => (x.Id != project.Id) && string.Equals(x.Title.ToLower(), project.Title.ToLower()));
        if (result != null) throw new BusinessException(ProjectMessages.ProjeMevcut);
    }
}