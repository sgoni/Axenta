public class InactiveAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeactivateAccountCommand, DesactivateAccountResult>
{
    public async Task<DesactivateAccountResult> Handle(DeactivateAccountCommand command,
        CancellationToken cancellationToken)
    {
        var accountId = AccountId.Of(command.accountId);
        var account = await dbContext.Accounts.FindAsync([accountId], cancellationToken);

        if (account is null) throw new AccountNotFoundException(command.accountId);

        account.Deactivate();
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DesactivateAccountResult(true);
    }
}