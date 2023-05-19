using asari.com.tr.Application.Features.OperationClaims.Queries.GetById;
using asari.com.tr.Application.Features.OperationClaims.Rules;
using asari.com.tr.Application.Features.OperationClaims.Rules;
using asari.com.tr.Application.Services.Repositories;
using asari.com.tr.Domain.Entities;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace asari.com.tr.Application.Features.OperationClaims.Queries.GetById;

public class GetByIdOperationClaimQuery:IRequest<GetByIdOperationClaimGetByIdResponse>
{
    public int Id { get; set; }

    public class GetByIdOperationClaimQueryHandler : IRequestHandler<GetByIdOperationClaimQuery, GetByIdOperationClaimGetByIdResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public GetByIdOperationClaimQueryHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimRules;
        }

        public async Task<GetByIdOperationClaimGetByIdResponse> Handle(GetByIdOperationClaimQuery request, CancellationToken cancellationToken)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);
            _operationClaimBusinessRules.OperationClaimShouldExistWhenRequested(operationClaim);

            GetByIdOperationClaimGetByIdResponse mappedGetByIdOperationClaimGetByIdResponse = _mapper.Map<GetByIdOperationClaimGetByIdResponse>(operationClaim);

            return mappedGetByIdOperationClaimGetByIdResponse;
        }
    }
}