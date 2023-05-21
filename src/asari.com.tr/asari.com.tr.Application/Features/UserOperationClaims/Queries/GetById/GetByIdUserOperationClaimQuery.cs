using asari.com.tr.Application.Features.UserOperationClaims.Queries.GetById;
using asari.com.tr.Application.Features.UserOperationClaims.Rules;
using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace asari.com.tr.Application.Features.UserOperationClaims.Queries.GetById;

public class GetByIdUserOperationClaimQuery : IRequest<GetByIdUserOperationClaimResponse>
{
    public int Id { get; set; }

    public class GetByIdUserOperationClaimQueryHandler : IRequestHandler<GetByIdUserOperationClaimQuery, GetByIdUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public GetByIdUserOperationClaimQueryHandler(IUserOperationClaimRepository UserOperationClaimRepository, IMapper mapper, UserOperationClaimBusinessRules UserOperationClaimRules)
        {
            _userOperationClaimRepository = UserOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = UserOperationClaimRules;
        }

        public async Task<GetByIdUserOperationClaimResponse> Handle(GetByIdUserOperationClaimQuery request, CancellationToken cancellationToken)
        {
            UserOperationClaim? userOperationClaim = await _userOperationClaimRepository.GetAsync(x => x.Id == request.Id);
            _userOperationClaimBusinessRules.UserOperationClaimShouldExistWhenRequested(userOperationClaim);

            GetByIdUserOperationClaimResponse mappedGetByIdUserOperationClaimGetByIdResponse = _mapper.Map<GetByIdUserOperationClaimResponse>(userOperationClaim);

            return mappedGetByIdUserOperationClaimGetByIdResponse;
        }
    }
}