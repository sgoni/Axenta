namespace Accounting.Application.Accounting.Accounts.Commands.ActiveAccount;

public class ActiveAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<ActiveAccountCommand, ActiveAccountResult>
{
    public async Task<ActiveAccountResult> Handle(ActiveAccountCommand command, CancellationToken cancellationToken)
    {
        var accountId = AccountId.Of(command.accountId);
        var account = await dbContext.Accounts.FindAsync([accountId], cancellationToken);

        if (account is null) throw EntityNotFoundException.For<Account>(command.accountId);

        account.Activate();
        await dbContext.SaveChangesAsync(cancellationToken);

        return new ActiveAccountResult(true);
    }
}