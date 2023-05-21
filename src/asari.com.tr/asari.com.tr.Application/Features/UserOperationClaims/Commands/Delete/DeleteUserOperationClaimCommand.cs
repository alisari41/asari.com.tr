using asari.com.tr.Application.Features.UserOperationClaims.Constants;
using asari.com.tr.Application.Features.UserOperationClaims.Rules;
using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Security.Entities;
using MediatR;
using static asari.com.tr.Application.Features.UserOperationClaims.Constants.UserOperationClaimsOperationClaims;

namespace asari.com.tr.Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommand : IRequest<DeletedUserOperationClaimResponse>, ISecuredRequest, ICacheRemoverRequest
{
    public int Id { get; set; }

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[] CacheGroupKey => new[] { CacheGroupKeyValue.UserOperationClaimCacheGroupKey };

    public string[] Roles => new[] { Admin, Write, UserOperationClaimsOperationClaims.Delete };

    public class DeleteUserOperationClaimCommandHandler : IRequestHandler<DeleteUserOperationClaimCommand, DeletedUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public DeleteUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<DeletedUserOperationClaimResponse> Handle(DeleteUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(x => x.Id == request.Id);

            _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenRequested(userOperationClaim);

            _mapper.Map(request, userOperationClaim);
            UserOperationClaim deletedUserOperationClaim = await _userOperationClaimRepository.DeleteAsync(userOperationClaim);
            DeletedUserOperationClaimResponse mappedDeletedUserOperationClaimResponse = _mapper.Map<DeletedUserOperationClaimResponse>(deletedUserOperationClaim);

            return mappedDeletedUserOperationClaimResponse;
        }
    }
}