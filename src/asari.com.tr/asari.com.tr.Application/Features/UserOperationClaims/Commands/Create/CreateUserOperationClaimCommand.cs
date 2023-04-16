using asari.com.tr.Application.Features.Auths.Rules;
using asari.com.tr.Application.Features.OperationClaims.Rules;
using asari.com.tr.Application.Features.UserOperationClaims.Rules;
using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Security.Entities;
using MediatR;
using static asari.com.tr.Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace asari.com.tr.Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string? CacheGroupKey => CacheGroupKeyValue.UserOperationClaimCacheGroupKey;

    public string[] Roles => new[] { Admin, Write, Add };

    public class CreateUserOperationClaimCommandHandler : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public CreateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules, AuthBusinessRules authBusinessRules, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            _authBusinessRules = authBusinessRules;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<CreatedUserOperationClaimResponse> Handle(CreateUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            await _userOperationClaimBusinessRules.UserOperationClaimConNotBeDuplicatedWhenInserted(request.UserId, request.OperationClaimId);
            await _authBusinessRules.UserShouldExistWhenRequested(request.UserId);
            await _operationClaimBusinessRules.OperationClaimShouldExistWhenRequested(request.OperationClaimId);

            UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);
            UserOperationClaim createdUserOperationClaim = await _userOperationClaimRepository.AddAsync(mappedUserOperationClaim);
            CreatedUserOperationClaimResponse mappedCreatedUserOperationClaimResponse = _mapper.Map<CreatedUserOperationClaimResponse>(createdUserOperationClaim);

            return mappedCreatedUserOperationClaimResponse;
        }
    }
}