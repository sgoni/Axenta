namespace Accounting.Application.Extensions;

public static class AccountExtensions
{
    public static IEnumerable<AccountDto> ToAccountDtoList(this IEnumerable<Account> accounts)
    {
        return accounts.Select(account => new AccountDto(
            account.Id.Value,
            account.Code,
            account.Name,
            account.AccountTypeId.Value,
            account.IsActive
        ));
    }

    public static AccountDetailDto ToAccountDto(this Account account)
    {
        return DtoFromAccount(account);
    }

    private static AccountDetailDto DtoFromAccount(Account account)
    {
        return new AccountDetailDto(
            account.Id.Value,
            account.Code,
            account.Name,
            account.AccountTypeId.Value,
            account.ParentId.Value,
            account.IsActive);
    }
}