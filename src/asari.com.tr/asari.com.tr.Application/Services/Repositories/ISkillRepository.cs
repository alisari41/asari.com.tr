using asari.com.tr.Domain.Entities;
using Core.Persistence.Repositories;

namespace asari.com.tr.Application.Services.Repositories;

public interface ISkillRepository : IAsyncRepository<Skill>, IRepository<Skill>
{
}
