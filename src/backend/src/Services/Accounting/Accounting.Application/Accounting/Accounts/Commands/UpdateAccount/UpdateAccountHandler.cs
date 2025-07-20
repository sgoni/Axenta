namespace Accounting.Application.Accounting.Accounts.Commands.UpdateAccount;

public class UpdateAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateAccountCommand, UpdateAccountResult>
{
    public async Task<UpdateAccountResult> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        //Update Order entity from command object
        //save to database
        //return result

        var accountId = AccountId.Of(command.Account.Id);
        var account = await dbContext.Accounts.FindAsync([accountId], cancellationToken);

        if (account is null) throw new AccountNotFoundException(command.Account.Id);

        UpdateAccountWithNewValues(account, command.Account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateAccountResult(true);
    }

    private void UpdateAccountWithNewValues(Account account, AccountDto accountDto)
    {
        account.Update(
            accountDto.Name,
            accountDto.Code,
            AccountTypeId.Of(accountDto.AccountTypeId),
            AccountId.Of(accountDto.ParentId),
            accountDto.IsActive);
    }
}