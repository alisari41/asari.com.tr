using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;

namespace asari.com.tr.Application.Features.Projects.Rules;

public class ProjectRules
{
    private readonly IProjectRepository _projectRepository;

    public ProjectRules(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public void ProgrammingLanguageShouldExistWhenRequested(Project? project)
    {
        if (project == null) throw new BusinessException("Proje mevcut değildir.");
    }

    public async Task ProjectShouldExistWhenRequested(int id)
    {
        Project? result = await _projectRepository.GetAsync(x => x.Id == id, enableTracking: false);
        ProgrammingLanguageShouldExistWhenRequested(result);
    }

    public async Task ProjectTitleConNotBeDuplicatedWhenInserted(string title)
    {
        Project? result = await _projectRepository.GetAsync(x => string.Equals(x.Title.ToLower(), title.ToLower())); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException("Proje Başlığı kullanılmaktadır!");
    }

    public async Task ProjectTitleConNotBeDuplicatedWhenUpdated(Project project)
    {
        Project? result = await _projectRepository.GetAsync(x => (x.Id != project.Id) && string.Equals(x.Title.ToLower(), project.Title.ToLower()));
        if (result != null) throw new BusinessException("Proje Başlığı kullanılmaktadır!");
    }
}