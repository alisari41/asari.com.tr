using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using asari.com.tr.Persistence.Contexts;
using Core.Persistence.Repositories;

namespace asari.com.tr.Persistence.Repositories;

public class EducationRepository : EfRepositoryBase<Education, BaseDbContext>, IEducationRepository
{
    public EducationRepository(BaseDbContext context) : base(context)
    {
        // BaseDbContext'i Ef içerisinde Base Context e yolladı
    }
}
