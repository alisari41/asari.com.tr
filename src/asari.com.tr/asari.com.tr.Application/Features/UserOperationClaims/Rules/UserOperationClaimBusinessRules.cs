using asari.com.tr.Application.Services.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;

namespace asari.com.tr.Application.Features.UserOperationClaims.Rules;

public class UserOperationClaimBusinessRules : BaseBusinessRules
{
    private readonly IUserOperationClaimRepository _userOperationClaimRepository;

    public UserOperationClaimBusinessRules(IUserOperationClaimRepository userOperationClaimRepository)
    {
        _userOperationClaimRepository = userOperationClaimRepository;
    }
    public void UserOperationClaimShouldExistWhenRequested(UserOperationClaim? UserOperationClaim)
    {
        if (UserOperationClaim == null) throw new BusinessException("Kullanıcı Rolü mevcut değildir.");
    }

    public async Task UserOperationClaimShouldExistWhenRequested(int id)
    {
        UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(x => x.Id == id, enableTracking: false);
        UserOperationClaimShouldExistWhenRequested(result);
    }

    public async Task UserOperationClaimConNotBeDuplicatedWhenInserted(int userId, int operationClaimId)
    {
        UserOperationClaim? result = await _userOperationClaimRepository.GetAsync(x => (x.UserId == userId) && (x.OperationClaimId == operationClaimId));
        if (result != null) throw new BusinessException("Kullanıcı Rolü kullanılmaktadır!");
    }
}