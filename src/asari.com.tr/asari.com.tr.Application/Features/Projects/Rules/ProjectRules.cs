using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using Core.CrossCuttingConcerns.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.Projects.Rules;

public class ProjectRules
{
    private readonly IProjectRepository _projectRepository;

    public ProjectRules(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task ProjectShouldExistWhenRequested(int id)
    {
        // Eğer biz aradığımız Id ile tüm listenin verilerini istemiyorsa sadece kontrol sağlamak için bu metot kullanılır. Yok biz id vererek GetById (id numarasını vererek tüm listeyi almak gibi) gibi aramalar yapmak isteyip birde sorgulama yapmak istersek yukardaki metot kullanılır
        var result = await _projectRepository.Query().Where(x => x.Id == id).AnyAsync();
        if (!result) throw new BusinessException("Proje mevcut değildir.");
    }

    public async Task ProjectTitleConNotBeDuplicatedWhenInserted(string title)
    {
        var result = await _projectRepository.Query().Where(x => x.Title.ToLower() == title.ToLower()).AnyAsync(); // Aynı isimde veri var mı
        if (result) throw new BusinessException("Proje Başlığı kullanılmaktadır!");
    }

    public async Task ProjectTitleConNotBeDuplicatedWhenUpdated(Project? project)
    {
        var result = await _projectRepository.Query().Where(x => (x.Id != project.Id) && (x.Title.ToLower() == project.Title.ToLower())).AnyAsync();

        if (result) throw new BusinessException("Proje Başlığı kullanılmaktadır!");
    }
}