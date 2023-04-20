using asari.com.tr.Application.Features.OperationClaims.Contants;
using asari.com.tr.Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Entities;

namespace asari.com.tr.Application.Features.OperationClaims.Rules;

public class OperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IOperationClaimRepository _operationClaimRepository;

    public OperationClaimBusinessRules(IOperationClaimRepository operationClaimRepository)
    {
        _operationClaimRepository = operationClaimRepository;
    }

    public void OperationClaimShouldExistWhenRequested(OperationClaim? operationClaim)
    {
        if (operationClaim == null) throw new BusinessException(OperationClaimMessages.RolMevcutDegil);
    }

    public async Task OperationClaimShouldExistWhenRequested(int id)
    {
        OperationClaim? result = await _operationClaimRepository.GetAsync(x => x.Id == id, enableTracking: false);
        OperationClaimShouldExistWhenRequested(result);
    }

    public async Task OperationClaimNameCanNotBeDuplacatedWhenInserted(string name)
    {
        OperationClaim? result = await _operationClaimRepository.GetAsync(x => string.Equals(x.Name.ToLower(), name.ToLower()));
        if (result != null) throw new BusinessException(OperationClaimMessages.RolMevcut);
    }

    public async Task OperationClaimNameCanNotBeDuplacatedWhenUpdated(OperationClaim operationClaim)
    {
        OperationClaim? result = await _operationClaimRepository.GetAsync(x => (x.Id != operationClaim.Id) && string.Equals(x.Name.ToLower(), operationClaim.Name.ToLower()));
        if (result != null) throw new BusinessException(OperationClaimMessages.RolMevcut);
    }
}