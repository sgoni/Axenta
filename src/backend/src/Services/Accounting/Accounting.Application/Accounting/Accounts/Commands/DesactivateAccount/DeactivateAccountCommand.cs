﻿namespace Accounting.Application.Accounting.Accounts.Commands.DesactivateAccount;

public record DeactivateAccountCommand(Guid accountId) : ICommand<DesactivateAccountResult>;

public record DesactivateAccountResult(bool IsSuccess);

public class DesactiveAccountCommandValidator : AbstractValidator<DeactivateAccountCommand>
{
    public DesactiveAccountCommandValidator()
    {
        RuleFor(x => x.accountId).NotEmpty().WithMessage("AccountId is required");
    }
}