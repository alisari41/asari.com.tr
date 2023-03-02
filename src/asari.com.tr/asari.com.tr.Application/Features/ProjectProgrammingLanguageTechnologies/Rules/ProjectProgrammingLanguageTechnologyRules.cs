using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.ProjectProgrammingLanguageTechnologies.Rules;

public class ProjectProgrammingLanguageTechnologyRules
{
    private readonly IProjectProgrammingLanguageTechnologyRepository _projectProgrammingLanguageTechnologyRepository;

    public ProjectProgrammingLanguageTechnologyRules(IProjectProgrammingLanguageTechnologyRepository projectProgrammingLanguageTechnologyRepository)
    {
        _projectProgrammingLanguageTechnologyRepository = projectProgrammingLanguageTechnologyRepository;
    }

    public async Task ProjectProgrammingLanguageTechnologyShouldExistWhenRequested(int id)
    {
        // Eğer biz aradığımız Id ile tüm listenin verilerini istemiyorsa sadece kontrol sağlamak için bu metot kullanılır. Yok biz id vererek GetById (id numarasını vererek tüm listeyi almak gibi) gibi aramalar yapmak isteyip birde sorgulama yapmak istersek yukardaki metot kullanılır
        var result = await _projectProgrammingLanguageTechnologyRepository.Query().Where(x => x.Id == id).AnyAsync();
        if (!result) throw new BusinessException("Proje Programlama dili mevcut değildir.");
    }
    public async Task ProjectProgrammingLanguageTechnologySConNotBeDuplicatedWhenInserted(int programmingLanguageTechnologyId, int projectId)
    {
        var result = await _projectProgrammingLanguageTechnologyRepository.Query().Where(x => (x.ProgrammingLanguageTechnologyId == programmingLanguageTechnologyId) && (x.ProjectId == projectId)).AnyAsync(); // Aynı isimde veri var mı
        if (result) throw new BusinessException("Proje Programalama Dili kullanılmaktadır!");
    }
    public async Task ProjectProgrammingLanguageTechnologySConNotBeDuplicatedWhenUpdated(ProjectProgrammingLanguageTechnology projectProgrammingLanguageTechnology)
    {
        var result = await _projectProgrammingLanguageTechnologyRepository.Query().Where(x => (x.Id != projectProgrammingLanguageTechnology.Id) && (x.ProgrammingLanguageTechnologyId == projectProgrammingLanguageTechnology.ProgrammingLanguageTechnologyId) && (x.ProjectId == projectProgrammingLanguageTechnology.ProjectId)).AnyAsync(); // Aynı isimde veri var mı
        if (result) throw new BusinessException("Proje Programalama Dili kullanılmaktadır!");
    }
}
