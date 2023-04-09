using asari.com.tr.Application.Features.UserOperationClaims.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommandValidator : AbstractValidator<CreateUserOperationClaimCommand>
{
    public CreateUserOperationClaimCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.UserId).NotEmpty().WithMessage(UserOperationClaimMessages.UserIdBosOlmamali);
        RuleFor(x => x.OperationClaimId).NotEmpty().WithMessage(UserOperationClaimMessages.OperationClaimIdBosOlmamali);
        #endregion
    }
}