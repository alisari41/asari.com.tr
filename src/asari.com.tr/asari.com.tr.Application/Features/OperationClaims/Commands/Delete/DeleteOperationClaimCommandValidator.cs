using FluentValidation;

namespace asari.com.tr.Application.Features.OperationClaims.Commands.Delete;

public class DeleteOperationClaimCommandValidator : AbstractValidator<DeleteOperationClaimCommand>
{
    public DeleteOperationClaimCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("'Rol Id'si boş bırakmayınız");
    }
}