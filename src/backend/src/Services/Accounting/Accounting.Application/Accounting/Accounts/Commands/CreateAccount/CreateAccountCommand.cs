namespace Accounting.Application.Accounting.Accounts.Commands.CreateAccount;

public record CreateAccountCommand(AccountDto AccountDetail) : ICommand<CreateAccountResult>;

public record CreateAccountResult(Guid Id);

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.AccountDetail.AccountTypeId).NotEmpty().WithMessage("Type Id is required");
        RuleFor(x => x.AccountDetail.Code).NotNull().MaximumLength(50).WithMessage("Code Id is required");
        RuleFor(x => x.AccountDetail.Name).NotNull().MaximumLength(150).WithMessage("Name Id is required");
    }
}