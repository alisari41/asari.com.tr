using asari.com.tr.Application.Features.Auths.Rules;
using asari.com.tr.Application.Features.OperationClaims.Rules;
using asari.com.tr.Application.Features.UserOperationClaims.Rules;
using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace asari.com.tr.Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommand : IRequest<UpdatedUserOperationClaimResponse>
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OperationClaimId { get; set; }

    public class UpdateUserOperationClaimCommandHandler : IRequestHandler<UpdateUserOperationClaimCommand, UpdatedUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public UpdateUserOperationClaimCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules userOperationClaimBusinessRules, AuthBusinessRules authBusinessRules, OperationClaimBusinessRules operationClaimBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
            _authBusinessRules = authBusinessRules;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<UpdatedUserOperationClaimResponse> Handle(UpdateUserOperationClaimCommand request, CancellationToken cancellationToken)
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(x => x.Id == request.Id, enableTracking: false);

            _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenRequested(userOperationClaim);

            _mapper.Map(request, userOperationClaim);

            await _userOperationClaimBusinessRules.UserOperationClaimConNotBeDuplicatedWhenUpdated(userOperationClaim);
            await _authBusinessRules.UserShouldExistWhenRequested(request.UserId);
            await _operationClaimBusinessRules.OperationClaimShouldExistWhenRequested(request.OperationClaimId);

            UserOperationClaim updatedUserOperationClaim = await _userOperationClaimRepository.UpdateAsync(userOperationClaim);
            UpdatedUserOperationClaimResponse mappedUpdatedUserOperationClaimResponse = _mapper.Map<UpdatedUserOperationClaimResponse>(updatedUserOperationClaim);

            return mappedUpdatedUserOperationClaimResponse;
        }
    }
}