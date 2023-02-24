using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProgrammingLanguages.Rules;

public class ProgrammingLanguageRules
{
    private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

    public ProgrammingLanguageRules(IProgrammingLanguageRepository programmingLanguageRepository)
    {
        _programmingLanguageRepository = programmingLanguageRepository;
    }

    public void ProgrammingLanguageShouldExistWhenRequested(ProgrammingLanguage? programmingLanguage)
    {
        // Eğer bir Programlama Dili  talep ediliyorsa o dilin olması gerekir.

        if (programmingLanguage == null) throw new BusinessException("Programlama dili mevcut değildir.");
    }

    public async Task ProgrammingLanguageShouldExistWhenRequested(int id)
    {
        var result = await _programmingLanguageRepository.Query().Where(x => x.Id == id).AnyAsync();
        if (!result) throw new BusinessException("Programlama dili mevcut değildir.");
    }

    public async Task ProgrammingLanguageConNotBeDuplicatedWhenInserted(string name)
    {
        var result = await _programmingLanguageRepository.Query().Where(x => x.Name.ToLower() == name.ToLower()).AnyAsync(); // Aynı isimde veri var mı
        if (result) throw new BusinessException("Progralama Dili kullanılmaktadır!"); // BusinessException için "Core.CrossCuttingConcerns" dan Referans almak gerekir
    }
}
