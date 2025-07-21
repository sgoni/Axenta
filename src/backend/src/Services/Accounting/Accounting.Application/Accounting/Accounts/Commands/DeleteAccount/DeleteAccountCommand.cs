namespace Accounting.Application.Accounting.Accounts.Commands.DeleteAccount;

public record DeleteAccountCommand(Guid accountId) : ICommand<DeleteAccountResult>;

public record DeleteAccountResult(bool IsSuccess);

public class DeleteAccountCommandValidator : AbstractValidator<DeleteAccountCommand>
{
    public DeleteAccountCommandValidator()
    {
        RuleFor(x => x.accountId).NotEmpty().WithMessage("AccountId is required");
    }
}