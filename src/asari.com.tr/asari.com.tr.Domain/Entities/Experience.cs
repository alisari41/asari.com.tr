using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class Experience : Entity
{
    public string Title { get; set; }
    public string EmploymentType { get; set; }
    public string CompanyName { get; set; }
    public string Location { get; set; }
    public bool Statu { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Industry { get; set; }
    public string Description { get; set; }
    public string? ProfileHeadline { get; set; }

    public virtual ICollection<ExperienceSkill> ExperienceSkills { get; set; }

    public Experience()
    {

    }

    public Experience(int id, string title, string employmentType, string companyName, string location, bool statu, DateTime startDate, DateTime? endDate, string industry, string description, string? profileHeadline) : this()
    {
        Id = id;
        Title = title;
        EmploymentType = employmentType;
        CompanyName = companyName;
        Location = location;
        Statu = statu;
        StartDate = startDate;
        EndDate = endDate;
        Industry = industry;
        Description = description;
        ProfileHeadline = profileHeadline;
    }
}