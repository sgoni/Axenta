namespace Accounting.Application.Accounting.Accounts.Commands.DeleteAccount;

public class DeleteAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteAccountCommand, DeleteAccountResult>
{
    public async Task<DeleteAccountResult> Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
    {
        //Delete Account entity from command object
        //save to database
        //return result

        var accountId = AccountId.Of(command.accountId);
        var account = await dbContext.Accounts.FindAsync([accountId], cancellationToken);

        if (account is null) throw new AccountNotFoundException(command.accountId);

        dbContext.Accounts.Remove(account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteAccountResult(true);
    }
}