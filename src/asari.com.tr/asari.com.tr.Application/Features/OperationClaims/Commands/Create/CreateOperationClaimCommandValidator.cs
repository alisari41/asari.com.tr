using asari.com.tr.Application.Features.OperationClaims.Contants;
using FluentValidation;

namespace asari.com.tr.Application.Features.OperationClaims.Commands.Create;

public class CreateOperationClaimCommandValidator : AbstractValidator<CreateOperationClaimCommand>
{
    public CreateOperationClaimCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage(OperationClaimMessages.NameBosOlmamali);
    }
}