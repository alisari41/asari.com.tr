using asari.com.tr.Application.Features.OperationClaims.Rules;
using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Security.Entities;
using MediatR;
using static asari.com.tr.Application.Features.OperationClaims.Contants.OperationClaimsOperationClaims;

namespace asari.com.tr.Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public string Name { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.OperationClaimCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateOperationClaimCommandHandler : IRequestHandler<CreateOperationClaimCommand, CreatedOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimRules;

        public CreateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimRules = operationClaimRules;
        }

        public async Task<CreatedOperationClaimResponse> Handle(CreateOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _operationClaimRules.OperationClaimNameCanNotBeDuplacatedWhenInserted(request.Name);

            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
            OperationClaim createOperationClaim = await _operationClaimRepository.AddAsync(mappedOperationClaim);
            CreatedOperationClaimResponse CreatedOperationClaimResponse = _mapper.Map<CreatedOperationClaimResponse>(createOperationClaim);

            return CreatedOperationClaimResponse;
        }
    }
}