using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProgrammingLanguageTechnologies.Rules;

public class ProgrammingLanguageTechnologyRules
{
    private readonly IProgrammingLanguageTechnologyRepository _programmingLanguageTechnologyRepository;

    public ProgrammingLanguageTechnologyRules(IProgrammingLanguageTechnologyRepository programmingLanguageTechnologyRepository)
    {
        _programmingLanguageTechnologyRepository = programmingLanguageTechnologyRepository;
    }

    public async Task TechnologyShouldExistWhenRequested(int id)
    {
        var result = await _programmingLanguageTechnologyRepository.Query().Where(x => x.Id == id).AnyAsync();
        if (!result) throw new BusinessException("Programlama dili Teknolojisi mevcut değildir.");
    }

    public async Task TechnologyNameCanNotBeDuplicatedWhenIserted(string name)
    {
        var result = await _programmingLanguageTechnologyRepository.Query().Where(x => x.Name.ToLower() == name.ToLower()).AnyAsync(); // Aynı isimde veri var mı
        if (result) throw new BusinessException("Programlama Dili Teknolojisi kullanılmaktadır.");
    }

    public async Task TechnologyNameConNotBeDuplicatedWhenUpdated(ProgrammingLanguageTechnology? programmingLanguageTechnology)
    {
        var result = await _programmingLanguageTechnologyRepository.Query().Where(x => (x.Id != programmingLanguageTechnology.Id) && (x.Name.ToLower() == programmingLanguageTechnology.Name.ToLower())).AnyAsync();

        if (result) throw new BusinessException("Programlama Dili Teknolojisi kullanılmaktadır.");
    }
}
