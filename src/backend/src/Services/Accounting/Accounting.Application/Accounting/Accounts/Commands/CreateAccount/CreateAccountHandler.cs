namespace Accounting.Application.Accounting.Accounts.Commands.CreateAccount;

public class CreateAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateAccountCommand, CreateAccountResult>
{
    public async Task<CreateAccountResult> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        //Create account entity from command object
        //Save to database
        //return result

        var account = CreateNewAccount(command.Account);
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateAccountResult(account.Id.Value);
    }

    private Account CreateNewAccount(AccountDto accountDto)
    {
        var newAccount =
            Account.Create(AccountId.Of(Guid.NewGuid()),
                accountDto.Code,
                accountDto.Name,
                AccountTypeId.Of(accountDto.AccountTypeId),
                AccountId.Of(accountDto.ParentId));

        return newAccount;
    }
}