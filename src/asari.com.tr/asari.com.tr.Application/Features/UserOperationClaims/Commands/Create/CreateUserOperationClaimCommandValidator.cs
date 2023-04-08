using FluentValidation;

namespace asari.com.tr.Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommandValidator : AbstractValidator<CreateUserOperationClaimCommand>
{
    public CreateUserOperationClaimCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı Id'si boş bırakmayınız");
        RuleFor(x => x.OperationClaimId).NotEmpty().WithMessage("Rol Id'si boş bırakmayınız");
        #endregion
    }
}