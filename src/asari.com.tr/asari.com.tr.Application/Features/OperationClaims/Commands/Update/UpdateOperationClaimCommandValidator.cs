using asari.com.tr.Application.Features.OperationClaims.Contants;
using FluentValidation;

namespace asari.com.tr.Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommandValidator : AbstractValidator<UpdateOperationClaimCommand>
{
    public UpdateOperationClaimCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage(OperationClaimMessages.IdBosOlmamali);
        RuleFor(x => x.Name).NotEmpty().WithMessage(OperationClaimMessages.NameBosOlmamali);
    }
}