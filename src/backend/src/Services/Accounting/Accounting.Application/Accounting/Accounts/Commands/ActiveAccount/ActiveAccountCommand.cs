namespace Accounting.Application.Accounting.Accounts.Commands.ActiveAccount;

public record ActiveAccountCommand(Guid accountId) : ICommand<ActiveAccountResult>;

public record ActiveAccountResult(bool IsSuccess);

public class ActiveAccountCommandValidator : AbstractValidator<ActiveAccountCommand>
{
    public ActiveAccountCommandValidator()
    {
        RuleFor(x => x.accountId).NotEmpty().WithMessage("AccountId is required");
    }
}