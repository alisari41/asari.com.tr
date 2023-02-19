using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using asari.com.tr.Persistence.Contexts;
using Core.Persistence.Repositories;

namespace asari.com.tr.Persistence.Repositories;

public class ProgrammingLanguageTechnologyRepository : EfRepositoryBase<ProgrammingLanguageTechnology, BaseDbContext>, IProgrammingLanguageTechnologyRepository
{
    public ProgrammingLanguageTechnologyRepository(BaseDbContext context) : base(context)
    {
        // BaseDbContext'i Ef içerisinde Base Context e yolladı
    }
}
