using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;

namespace asari.com.tr.Application.Features.TechnologyProjects.Rules;

public class TechnologyProjectBusinessRules : BaseBusinessRules
{
    private readonly ITechnologyProjectRepository _technologyProjectRepository;

    public TechnologyProjectBusinessRules(ITechnologyProjectRepository technologyProjectRepository)
    {
        _technologyProjectRepository = technologyProjectRepository;
    }


    public void TechnologyProjectShouldExistWhenRequested(TechnologyProject? technologyProject)
    {
        if (technologyProject == null) throw new BusinessException("Teknoloji Proje mevcut değildir.");
    }

    public async Task TechnologyProjectShouldExistWhenRequested(int id)
    {
        TechnologyProject? result = await _technologyProjectRepository.GetAsync(x => x.Id == id, enableTracking: false);
        TechnologyProjectShouldExistWhenRequested(result);
    }

    public async Task TechnologyProjectSConNotBeDuplicatedWhenInserted(int technologyId, int projectId)
    {
        TechnologyProject? result = await _technologyProjectRepository.GetAsync(x => (x.TechnologyId == technologyId) && (x.ProjectId == projectId));
        if (result != null) throw new BusinessException("Teknoloji Proje kullanılmaktadır!");
    }

    public async Task TechnologyProjectSConNotBeDuplicatedWhenUpdated(TechnologyProject technologyProject)
    {
        TechnologyProject? result = await _technologyProjectRepository.GetAsync(x => (x.Id != technologyProject.Id) && (x.TechnologyId == technologyProject.TechnologyId) && (x.ProjectId == technologyProject.ProjectId));
        if (result != null) throw new BusinessException("Teknoloji Proje kullanılmaktadır!");
    }
}