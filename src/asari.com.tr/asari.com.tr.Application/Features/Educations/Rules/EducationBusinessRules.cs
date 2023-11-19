using asari.com.tr.Application.Features.Educations.Constants;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;

namespace asari.com.tr.Application.Features.Educations.Rules;

public class EducationBusinessRules : BaseBusinessRules
{
    private readonly IEducationRepository _educationRepository;

    public EducationBusinessRules(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    public void EducationShouldExistWhenRequested(Education? education)
    {
        if (education == null) throw new BusinessException(EducationMessages.EgitimMevcutDegil);
    }

    public async Task EducationShouldExistWhenRequested(int? id)
    {
        Education? result = await _educationRepository.GetAsync(x => x.Id == id, enableTracking: false);
        EducationShouldExistWhenRequested(result);
    }
}