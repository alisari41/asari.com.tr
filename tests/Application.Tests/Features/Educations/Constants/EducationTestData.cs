﻿namespace Application.Tests.Features.Educations.Constants;

public static class EducationTestData
{
    #region Create
    public const string CreateName = "Derince Ticaret Meslek";
    public const string CreateFieldOfStudy = "Bilişim Teknolojileri / Veri Tabanı Programcılığı Dalı";
    public const string CreateGrade = "Lise";
    public static readonly DateTime CreateStartDate = new DateTime(2012, 9, 17);
    public static readonly DateTime CreateEndDateOrExcepted = new DateTime(2016, 6, 17);
    public const double CreateDegree = 79.56;
    public const string CreateActivityAndCommunity = null;
    public const string CreateDescription = null;
    public const string CreateMediaUrl = null;
    #endregion

    #region Update
    public const int UpdateId = 1;
    public const string UpdateName = "Updated - Derince Ticaret Meslek";
    public const string UpdateFieldOfStudy = "Updated - Bilişim Teknolojileri / Veri Tabanı Programcılığı Dalı";
    public const string UpdateGrade = "Updated - Lise";
    public static readonly DateTime UpdateStartDate = new DateTime(2012, 10, 18);
    public static readonly DateTime UpdateEndDateOrExcepted = new DateTime(2016, 7, 18);
    public const double UpdateDegree = 80.41;
    public const string UpdateActivityAndCommunity = "Updated - Activities";
    public const string UpdateDescription = "Updated - Description";
    public const string UpdateMediaUrl = "Updated - MediaUrl";
    #endregion

    #region Delete
    public const int DeleteId = 1;
    #endregion

    #region Tabloda Bulunmayan Id
    public const int NonexistentId = 41;
    #endregion

}