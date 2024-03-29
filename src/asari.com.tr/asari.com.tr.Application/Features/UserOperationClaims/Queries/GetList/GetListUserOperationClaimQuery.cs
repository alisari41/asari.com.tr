﻿using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace asari.com.tr.Application.Features.UserOperationClaims.Queries.GetList;

public class GetListUserOperationClaimQuery : IRequest<GetListResponse<GetListUserOperationClaimListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListUserOperationClaim({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.UserOperationClaimCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListUserOperationClaimQueryHandler : IRequestHandler<GetListUserOperationClaimQuery, GetListResponse<GetListUserOperationClaimListItemDto>>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;

        public GetListUserOperationClaimQueryHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListUserOperationClaimListItemDto>> Handle(GetListUserOperationClaimQuery request, CancellationToken cancellationToken)
        {
            IPaginate<UserOperationClaim> userOperationClaim = await _userOperationClaimRepository.GetListAsync(orderBy: x =>
                                                                                x.Include(c => c.User)
                                                                                 .Include(c => c.OperationClaim)
                                                                                 .OrderBy(c => c.User.Email),
                                                                                index: request.PageRequest.Page,
                                                                                size: request.PageRequest.PageSize);

            GetListResponse<GetListUserOperationClaimListItemDto> mappedGetListUserOperationClaimListItemDto = _mapper.Map<GetListResponse<GetListUserOperationClaimListItemDto>>(userOperationClaim);

            return mappedGetListUserOperationClaimListItemDto;
        }
    }
}