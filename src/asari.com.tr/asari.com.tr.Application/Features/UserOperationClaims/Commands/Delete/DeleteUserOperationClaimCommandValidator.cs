using asari.com.tr.Application.Features.UserOperationClaims.Constants;
using FluentValidation;

namespace asari.com.tr.Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommandValidator : AbstractValidator<DeleteUserOperationClaimCommand>
{
    public DeleteUserOperationClaimCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(UserOperationClaimMessages.IdBosOlmamali);
    }
}