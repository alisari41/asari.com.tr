using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageBusinessRules : BaseBusinessRules
{
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public ProgrammingLanguageBusinessRules(IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
    }

    public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage? programmingLanguage)
    {
        if (programmingLanguage == null) throw new BusinessException("Programlama dili mevcut değildir.");
    }

    public async Task ProgrammingLanguageShouldExistWhenRequested(int id)
    {
        ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(x => x.Id == id, enableTracking: false);
        ProgrammingLanguageShouldExistWhenRequested(result);
    }

    public async Task ProgrammingLanguageConNotBeDuplicatedWhenInserted(string name)
    {
        ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(x => string.Equals(x.Name.ToLower(),
                                                                                                            name.ToLower()));
        if (result != null) throw new BusinessException("Progralama Dili kullanılmaktadır!");
    }

    public async Task ProgrammingLanguageConNotBeDuplicatedWhenUpdated(ProgrammingLanguage programmingLanguage)
    {
        ProgrammingLanguage? result = await _programmingLanguageRepository.GetAsync(x => (x.Id != programmingLanguage.Id) && string.Equals(x.Name.ToLower(),
                                                                                                                                                programmingLanguage.Name.ToLower()));

        if (result != null) throw new BusinessException("Progralama Dili kullanılmaktadır!");
    }
}