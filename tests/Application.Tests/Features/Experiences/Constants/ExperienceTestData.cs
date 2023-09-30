namespace Application.Tests.Features.Experiences.Constants;

public static class ExperienceTestData
{
    #region Create
    public const string CreateTitle = "Software Engineer";
    public const string CreateEmploymentType = "Full-time";
    public const string CreateCompanyName = "ABC Inc.";
    public const string CreateLocation = "New York";
    public const bool CreateStatu = true;
    public static readonly DateTime CreateStartDate = new DateTime(2022, 1, 15);
    public static readonly DateTime? CreateEndDate = null;
    public const string CreateIndustry = "Technology";
    public const string CreateDescription = "Worked on software development projects.";
    public const string CreateProfileHeadline = "Experienced Software Engineer";
    #endregion

    #region Update
    public const int UpdateId = 1;
    public const string UpdateTitle = "Updated - Software Engineer";
    public const string UpdateEmploymentType = "Updated - Full-time";
    public const string UpdateCompanyName = "Updated - ABC Inc.";
    public const string UpdateLocation = "Updated - New York";
    public const bool UpdateStatu = false;
    public static readonly DateTime UpdateStartDate = new DateTime(2022, 5, 10);
    public static readonly DateTime? UpdateEndDate = new DateTime(2023, 1, 10);
    public const string UpdateIndustry = "Updated - Technology";
    public const string UpdateDescription = "Updated - Description of software engineering work.";
    public const string UpdateProfileHeadline = "Updated - Experienced Software Engineer";
    #endregion

    #region Delete
    public const int DeleteId = 1;
    #endregion

    #region Tabloda Bulunmayan Id
    public const int NonexistentId = 41;
    #endregion
}