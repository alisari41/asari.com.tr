using asari.com.tr.Application.Features.OperationClaims.Contants;
using asari.com.tr.Application.Features.OperationClaims.Rules;
using asari.com.tr.Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using static asari.com.tr.Application.Features.OperationClaims.Contants.OperationClaimsOperationClaims;

namespace asari.com.tr.Application.Features.OperationClaims.Commands.Delete;

public class DeleteOperationClaimCommand : IRequest<DeletedOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, Write, OperationClaimsOperationClaims.Delete };

    public class DeleteOperationClaimCommandHandler : IRequestHandler<DeleteOperationClaimCommand, DeletedOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimRules;

        public DeleteOperationClaimCommandHandler(IOperationClaimRepository operationClaimRepository, IMapper mapper, OperationClaimBusinessRules operationClaimRules)
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimRules = operationClaimRules;
        }

        public async Task<DeletedOperationClaimResponse> Handle(DeleteOperationClaimCommand request, CancellationToken cancellationToken)
        {
            OperationClaim? operationClaim = await _operationClaimRepository.GetAsync(x => x.Id == request.Id, enableTracking: false); // Silmek istediğim verinin bütün bilgilerine ihtiyacım olmadığı için kullanmadım

            _operationClaimRules.OperationClaimShouldExistWhenRequested(operationClaim);

            _mapper.Map(request, operationClaim);
            OperationClaim deletedOperationClaim = await _operationClaimRepository.DeleteAsync(operationClaim);
            DeletedOperationClaimResponse DeletedOperationClaimResponse = _mapper.Map<DeletedOperationClaimResponse>(deletedOperationClaim);

            return DeletedOperationClaimResponse;
        }
    }
}