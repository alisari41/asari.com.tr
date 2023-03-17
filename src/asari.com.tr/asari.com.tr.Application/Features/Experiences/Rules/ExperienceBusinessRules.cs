using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;

namespace asari.com.tr.Application.Features.Experiences.Rules;

public class ExperienceBusinessRules : BaseBusinessRules
{
    private readonly IExperienceRepository _experienceRepository;

    public ExperienceBusinessRules(IExperienceRepository experienceRepository)
    {
        _experienceRepository = experienceRepository;
    }

    public void ExperienceShouldExistWhenRequested(Experience? experience)
    {
        if (experience == null) throw new BusinessException("Deneyim mevcut değildir.");
    }

    public async Task ExperienceShouldExistWhenRequested(int id)
    {
        Experience? result = await _experienceRepository.GetAsync(x => x.Id == id, enableTracking: false);
        ExperienceShouldExistWhenRequested(result);
    }

    public async Task ExperienceTitleConNotBeDuplicatedWhenInserted(string title)
    {
        Experience? result = await _experienceRepository.GetAsync(x => string.Equals(x.Title.ToLower(), title.ToLower())); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException("Deneyim Başlığı kullanılmaktadır!");
    }
}