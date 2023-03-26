﻿using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.LicensesAndCertifications.Queries.GetList;

public class GetListLicenseAndCertificationListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string IssuingOrganization { get; set; }
    public DateTime? IssueDate { get; set; }
    public DateTime? ExpirationDate { get; set; }
    public string? ImagegUrl { get; set; }
    public string? CredentialId { get; set; }
    public string? CredentialUrl { get; set; }
}