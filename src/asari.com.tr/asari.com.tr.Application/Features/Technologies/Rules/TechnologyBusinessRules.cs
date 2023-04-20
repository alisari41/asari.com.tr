using asari.com.tr.Application.Features.Technologies.Constants;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace asari.com.tr.Application.Features.Technologies.Rules;

public class TechnologyBusinessRules : BaseBusinessRules
{
    private readonly ITechnologyRepository _technologyRepository;

    public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }


    public void TechnologyShouldExistWhenRequested(Technology? technology)
    {
        if (technology == null) throw new BusinessException(TechnologyMessages.TeknolojiMevcutDegil);
    }

    public async Task TechnologyShouldExistWhenRequested(int id)
    {
        Technology? result = await _technologyRepository.GetAsync(x => x.Id == id, enableTracking: false);
        TechnologyShouldExistWhenRequested(result);
    }

    public async Task TechnologyTitleConNotBeDuplicatedWhenInserted(string title)
    {
        Technology? result = await _technologyRepository.GetAsync(x => string.Equals(x.Title.ToLower(), title.ToLower())); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException(TechnologyMessages.TeknolojiMevcut);
    }

    public async Task TechnologyTitleConNotBeDuplicatedWhenUpdated(Technology technology)
    {
        Technology? result = await _technologyRepository.GetAsync(x => (x.Id != technology.Id) && (string.Equals(x.Title.ToLower(), technology.Title.ToLower()))); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException(TechnologyMessages.TeknolojiMevcut);
    }
}