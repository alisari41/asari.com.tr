namespace asari.com.tr.Application.Features.Educations.Queries.GetById;

public class GetByIdEducationResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double? Degree { get; set; }
    public string FieldOfStudy { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDateOrExcepted { get; set; }
    public string? Grade { get; set; }
    public string? ActivityAndCommunity { get; set; }
    public string? Description { get; set; }
    public string? MediaUrl { get; set; }
}