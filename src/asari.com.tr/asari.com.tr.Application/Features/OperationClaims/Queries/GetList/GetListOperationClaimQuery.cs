using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Caching;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Core.Security.Entities;
using MediatR;

namespace asari.com.tr.Application.Features.OperationClaims.Queries.GetList;

public class GetListOperationClaimQuery : IRequest<GetListResponse<GetListOperationClaimListItemDto>>, ICachableRequest
{
    public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

    public bool BypassCache { get; }
    public string CacheKey => $"GetListOperationClaim({PageRequest.Page},{PageRequest.PageSize})";
    public string? CacheGroupKey => CacheGroupKeyValue.OperationClaimCacheGroupKey;

    public TimeSpan? SlidingExpiration { get; }

    public class GetListOperationClaimQueryHandler : IRequestHandler<GetListOperationClaimQuery, GetListResponse<GetListOperationClaimListItemDto>>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;

        public GetListOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListOperationClaimListItemDto>> Handle(GetListOperationClaimQuery request, CancellationToken cancellationToken)
        {
            IPaginate<OperationClaim> operationClaims = await _operationClaimRepository.GetListAsync(orderBy: o => o.OrderBy(c => c.Name),
                                                                                                        index: request.PageRequest.Page, size: request.PageRequest.PageSize);

            var mappedOperationClaimListModel = _mapper.Map<GetListResponse<GetListOperationClaimListItemDto>>(operationClaims); // Dbden aldıklarını modeldaki Page kısmına atıyorum

            return mappedOperationClaimListModel;
        }
    }
}