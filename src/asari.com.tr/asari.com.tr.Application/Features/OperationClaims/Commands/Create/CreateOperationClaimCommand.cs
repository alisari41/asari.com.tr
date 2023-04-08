using asari.com.tr.Application.Features.OperationClaims.Rules;
using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace asari.com.tr.Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommand : IRequest<CreatedOperationClaimResponse>
{
    public string Name { get; set; }

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