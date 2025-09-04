namespace Accounting.Application.Accounting.Accounts.Commands.CreateAccount;

public class CreateAccountHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateAccountCommand, CreateAccountResult>
{
    public async Task<CreateAccountResult> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
    {
        //Create account entity from command object
        //Save to database
        //return result

        string newCode;
        Account? parentAccount = null;

        if (command.Account.ParentAccountId != null)
        {
            var parentAccountId = AccountId.FromNullable(command.Account.ParentAccountId);
            parentAccount = await dbContext.Accounts.FindAsync(parentAccountId);

            if (parentAccount is null)
                throw new Exception("Parent account does not exist.");

            // Find out how many children that father has
            var siblingCount = await dbContext.Accounts.CountAsync(a => a.ParentId == parentAccount.Id);

            // Eg: If there are two children, the next one will be number 3
            var suffix = (siblingCount + 1).ToString("D2"); // '01', '02', etc
            newCode = $"{parentAccount.Code}.{suffix}";
        }
        else
        {
            // Root account: generates its own unique root code
            var rootCount = await dbContext.Accounts.CountAsync(a => a.ParentId == null);
            newCode = (rootCount + 1).ToString(); // '1', '2'. '3', etc.
        }

        var account = CreateNewAccount(command.Account, parentAccount, newCode);
        dbContext.Accounts.Add(account);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateAccountResult(account.Id.Value);
    }

    private Account CreateNewAccount(AccountDto accountDetailDto, Account? parentAccount, string newCode)
    {
        var newAccount =
            Account.Create(
                AccountId.Of(Guid.NewGuid()),
                newCode,
                accountDetailDto.Name,
                AccountTypeId.Of(accountDetailDto.AccountTypeId),
                AccountId.FromNullable(accountDetailDto.ParentAccountId),
                accountDetailDto.Level == null ? 1 : parentAccount.Level + 1,
                accountDetailDto.IsMovable);

        return newAccount;
    }
}