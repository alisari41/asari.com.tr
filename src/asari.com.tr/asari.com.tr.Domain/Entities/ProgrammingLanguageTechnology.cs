using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class ProgrammingLanguageTechnology : Entity
{
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }
    public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; } // Bir teknolojinin Bir programlama dili olur 
                                                                          // Bir çok ORM için kullanılabilinmesi için "virtual" olarak süsledik

    // Programlama dili bir tane olduğu için ProgrammingLanguage şeklinde kullanıldı ilerde mesela progralama dilleri olsa List<...> şeklinde yazılır.

    public ProgrammingLanguageTechnology()
    {

    }

    public ProgrammingLanguageTechnology(int id, int progrramingLanguageId, string name) : this()
    {
        Id = id;
        ProgrammingLanguageId = progrramingLanguageId;
        Name = name;
    }
}
