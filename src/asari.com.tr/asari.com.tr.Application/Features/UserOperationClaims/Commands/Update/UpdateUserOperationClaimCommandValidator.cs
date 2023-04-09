using asari.com.tr.Application.Features.UserOperationClaims.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommandValidator : AbstractValidator<UpdateUserOperationClaimCommand>
{
    public UpdateUserOperationClaimCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage(UserOperationClaimMessages.IdBosOlmamali);
        RuleFor(x => x.UserId).NotEmpty().WithMessage(UserOperationClaimMessages.UserIdBosOlmamali);
        RuleFor(x => x.OperationClaimId).NotEmpty().WithMessage(UserOperationClaimMessages.OperationClaimIdBosOlmamali);
        #endregion
    }
}