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
        var account = await dbContext.Accounts
            .Include(a => a.Parent)
            .FirstOrDefaultAsync(a => a.Id == accountId);

        if (account is null) throw EntityNotFoundException.For<Account>(command.AccountDetail.Id);

        var childAccount = dbContext.Accounts
            .Include(a => a.Parent)
            .FirstOrDefault(a => a.ParentId == AccountId.FromNullable(command.AccountDetail.ParentAccountId));

        if (childAccount is null) throw new ConflictException("FK error parent account ID");

        UpdateAccountWithNewValues(account, command.AccountDetail);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateAccountResult(true);
    }

    private void UpdateAccountWithNewValues(Account account, AccountDto accountDetailDto)
    {
        account.Update(
            accountDetailDto.Name,
            accountDetailDto.Code,
            AccountTypeId.Of(accountDetailDto.AccountTypeId),
            AccountId.FromNullable(accountDetailDto.ParentAccountId),
            accountDetailDto.IsActive);
    }
}