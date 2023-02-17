using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class Skill : Entity
{
    public string Name { get; set; }

    public Skill()
    {

    }

    public Skill(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}
