namespace Accounting.Application.Accounting.Accounts.Commands.UpdateAccount;

public record UpdateAccountCommand(AccountDto Account) : ICommand<UpdateAccountResult>;

public record UpdateAccountResult(bool IsSuccess);

public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(x => x.Account.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.Account.AccountTypeId).NotEmpty().WithMessage("Type Id is required");
        RuleFor(x => x.Account.Code).NotNull().MaximumLength(50).WithMessage("Code Id is required");
        RuleFor(x => x.Account.Name).NotNull().MaximumLength(150).WithMessage("Name Id is required");
    }
}