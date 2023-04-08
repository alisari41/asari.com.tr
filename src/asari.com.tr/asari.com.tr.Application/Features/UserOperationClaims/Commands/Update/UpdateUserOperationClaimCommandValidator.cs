using FluentValidation;

namespace asari.com.tr.Application.Features.UserOperationClaims.Commands.Update;

public class UpdateUserOperationClaimCommandValidator : AbstractValidator<UpdateUserOperationClaimCommand>
{
    public UpdateUserOperationClaimCommandValidator()
    {
        #region Zorunlu Alanlar
        RuleFor(x => x.Id).NotEmpty().WithMessage("Kullanıcı Rol Id'si boş bırakmayınız");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("Kullanıcı Id'si boş bırakmayınız");
        RuleFor(x => x.OperationClaimId).NotEmpty().WithMessage("Rol Id'si boş bırakmayınız");
        #endregion
    }
}