using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class ProgrammingLanguage : Entity
{
    public string Name { get; set; }

    public virtual ICollection<ProgrammingLanguageTechnology> ProgrammingLanguageTechnologies { get; set; } // Bir programlama dilinin birden fazla teknolojisi olabileceği için bu şekilde yazıldı

    public ProgrammingLanguage()
    {

    }

    public ProgrammingLanguage(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }
}