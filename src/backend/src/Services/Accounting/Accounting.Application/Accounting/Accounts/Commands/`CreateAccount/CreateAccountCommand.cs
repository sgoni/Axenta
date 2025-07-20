namespace Accounting.Application.Accounting.Accounts.Commands._CreateAccount;

public record CreateAccountCommand(AccountDto Account) : ICommand<CreateAccountResult>;

public record CreateAccountResult(Guid Id);

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Account.AccountTypeId).NotNull();
        RuleFor(x => x.Account.Code).NotNull().MaximumLength(50);
        RuleFor(x => x.Account.Name).NotNull().MaximumLength(150);
    }
}