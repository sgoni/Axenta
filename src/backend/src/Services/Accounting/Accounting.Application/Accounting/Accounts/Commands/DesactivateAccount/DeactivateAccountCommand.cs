using Accounting.Application.Accounting.CostCenters.Commands.DeactivateCostCenter;

namespace Accounting.Application.Accounting.Accounts.Commands.DesactivateAccount;

public record DeactivateAccountCommand(Guid accountId)
    : ICommand<DesactivateAccountResult>, ICommand<DeactiveCostCenterResult>;

public record DesactivateAccountResult(bool IsSuccess);

public class DesactiveAccountCommandValidator : AbstractValidator<DeactivateAccountCommand>
{
    public DesactiveAccountCommandValidator()
    {
        RuleFor(x => x.accountId).NotEmpty().WithMessage("AccountId is required");
    }
}