﻿using Core.Application.Dtos;

namespace asari.com.tr.Application.Features.OperationClaims.Queries.GetList;

public class OperationClaimListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}