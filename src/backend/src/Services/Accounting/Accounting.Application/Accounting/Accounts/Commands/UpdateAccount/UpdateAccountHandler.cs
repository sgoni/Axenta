namespace Accounting.Application.Accounting.Accounts.Commands.UpdateAccount;

public class UpdateAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateAccountCommand, UpdateAccountResult>
{
    public async Task<UpdateAccountResult> Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
    {
        //Update account entity from command object
        //save to database
        //return result

        var accountId = AccountId.Of(command.AccountDetail.Id);
        var account = await dbContext.Accounts.FindAsync([accountId], cancellationToken);

        if (account is null) throw new AccountNotFoundException(command.AccountDetail.Id);

        UpdateAccountWithNewValues(account, command.AccountDetail);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateAccountResult(true);
    }

    private void UpdateAccountWithNewValues(Account account, AccountDetailDto accountDetailDto)
    {
        account.Update(
            accountDetailDto.Name,
            accountDetailDto.Code,
            AccountTypeId.Of(accountDetailDto.AccountTypeId),
            AccountId.Of(accountDetailDto.ParentId),
            accountDetailDto.IsActive);
    }
}