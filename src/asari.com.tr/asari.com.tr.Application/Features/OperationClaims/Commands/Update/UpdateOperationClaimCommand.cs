﻿using asari.com.tr.Application.Features.OperationClaims.Rules;
using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Security.Entities;
using MediatR;

namespace asari.com.tr.Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimResponse>
{
    public int Id { get; set; }
    public string Name { get; set; }

    public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimRules;

        public UpdateOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimRules = operationClaimRules;
        }

        public async Task<UpdatedOperationClaimResponse> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(x => x.Id == request.Id);

            _mapper.Map(request, operationClaim);

            _operationClaimRules.OperationClaimShouldExistWhenRequested(operationClaim);
            await _operationClaimRules.OperationClaimNameCanNotBeDuplacatedWhenUpdated(operationClaim);

            OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(operationClaim);
            UpdatedOperationClaimResponse UpdatedOperationClaimResponse = _mapper.Map<UpdatedOperationClaimResponse>(updatedOperationClaim);

            return UpdatedOperationClaimResponse;
        }
    }
}