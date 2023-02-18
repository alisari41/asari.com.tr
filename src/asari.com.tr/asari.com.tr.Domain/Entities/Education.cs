using Core.Persistence.Repositories;

namespace asari.com.tr.Domain.Entities;

public class Education : Entity
{
    public string Name { get; set; }
    public double Degree { get; set; }
    public string FieldOfStudy { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDateOrExcepted { get; set; }
    public string? Grade { get; set; }
    public string? ActivityAndCommunity { get; set; }
    public string? Description { get; set; }
    public string? MediaUrl { get; set; }

    public virtual ICollection<EducationSkill> EducationSkills { get; set; }

    public Education()
    {

    }

    public Education(int id, string name, double degree, string fieldOfStudy, DateTime startDate, DateTime? endDateOrExcepted, string? grade, string? activityAndCommunity, string? description, string? mediaUrl) : this()
    {
        Id = id;
        Name = name;
        Degree = degree;
        FieldOfStudy = fieldOfStudy;
        StartDate = startDate;
        EndDateOrExcepted = endDateOrExcepted;
        Grade = grade;
        ActivityAndCommunity = activityAndCommunity;
        Description = description;
        MediaUrl = mediaUrl;
    }
}