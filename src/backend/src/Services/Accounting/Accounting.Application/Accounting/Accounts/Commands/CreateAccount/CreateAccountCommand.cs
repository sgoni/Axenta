namespace Accounting.Application.Accounting.Accounts.Commands.CreateAccount;

public record CreateAccountCommand(AccountDto Account) : ICommand<CreateAccountResult>;

public record CreateAccountResult(Guid Id);

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Account.AccountTypeId).NotEmpty().WithMessage("Type Id is required");
        RuleFor(x => x.Account.Code).NotNull().MaximumLength(50).WithMessage("Code Id is required");
        RuleFor(x => x.Account.Name).NotNull().MaximumLength(150).WithMessage("Name Id is required");
    }
}