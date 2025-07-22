namespace Accounting.Application.Accounting.Accounts.Commands.UpdateAccount;

public record UpdateAccountCommand(AccountDetailDto AccountDetail) : ICommand<UpdateAccountResult>;

public record UpdateAccountResult(bool IsSuccess);

public class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(x => x.AccountDetail.Id).NotEmpty().WithMessage("Id is required");
        RuleFor(x => x.AccountDetail.AccountTypeId).NotEmpty().WithMessage("Type Id is required");
        RuleFor(x => x.AccountDetail.Code).NotNull().MaximumLength(50).WithMessage("Code Id is required");
        RuleFor(x => x.AccountDetail.Name).NotNull().MaximumLength(150).WithMessage("Name Id is required");
    }
}