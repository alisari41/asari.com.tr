using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;

public class ProgrammingLanguageTechnologyBusinessRules : BaseBusinessRules
{
    private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;

    public ProgrammingLanguageTechnologyBusinessRules(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository)
    {
        _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
    }

    public void ProgrammingLanguageTechnologyShouldExistWhenRequested(ProgrammingLanguageTechnology? programmingLanguageTechnology)
    {
        if (programmingLanguageTechnology == null) throw new BusinessException("Programlama dili Teknolojisi mevcut değildir.");
    }

    public async Task ProgrammingLanguageTechnologyShouldExistWhenRequested(int id)
    {
        ProgrammingLanguageTechnology? result = await _programmingLanguageTechnologyRepository.GetAsync(x => x.Id == id, enableTracking: false);
        ProgrammingLanguageTechnologyShouldExistWhenRequested(result);
    }

    public async Task ProgrammingLanguageTechnologyNameCanNotBeDuplicatedWhenIserted(string name)
    {
        ProgrammingLanguageTechnology? result = await _programmingLanguageTechnologyRepository.GetAsync(x => string.Equals(x.Name.ToLower(),
                                                                                                                                name.ToLower())); // Aynı isimde veri var mı
        if (result != null) throw new BusinessException("Programlama Dili Teknolojisi kullanılmaktadır.");
    }

    public async Task ProgrammingLanguageTechnologyNameConNotBeDuplicatedWhenUpdated(ProgrammingLanguageTechnology programmingLanguageTechnology)
    {
        ProgrammingLanguageTechnology? result = await _programmingLanguageTechnologyRepository.GetAsync(x => (x.Id != programmingLanguageTechnology.Id) && (string.Equals(x.Name.ToLower(),
                                                                                                                                                                               programmingLanguageTechnology.Name.ToLower())));

        if (result != null) throw new BusinessException("Programlama Dili Teknolojisi kullanılmaktadır.");
    }
}