using FluentValidation;

namespace asari.com.tr.Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommandValidator : AbstractValidator<UpdateOperationClaimCommand>
{
    public UpdateOperationClaimCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("'Rol Id'si boş bırakmayınız");
        RuleFor(x => x.Name).NotEmpty().WithMessage("'Rol Adı' boş bırakmayınız");
    }
}