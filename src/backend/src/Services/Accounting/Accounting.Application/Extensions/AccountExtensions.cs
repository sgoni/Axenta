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
            account.ParentId.Value,
            account.IsActive
        ));
    }

    public static AccountDto DtoFromAccount(this Account account)
    {
        return new AccountDto(
            account.Id.Value,
            account.Code,
            account.Name,
            account.AccountTypeId.Value,
            account.ParentId.Value,
            account.IsActive);
    }

    public static List<AccountTreeDto>? ToAccountTreeDtoList(this List<Account> accounts)
    {
        return accounts.Select(account => new AccountTreeDto(
            account.Id.Value,
            account.Code,
            account.Name,
            account.AccountTypeId.Value,
            account.ParentId.Value,
            new List<AccountTreeDto>()
        )) as List<AccountTreeDto>;
    }

    public static List<AccountTreeDto> AccountTreeDto(List<AccountTreeDto>? flatAccounts)
    {
        var lookup = flatAccounts.ToDictionary(x => x.Id);
        var roots = new List<AccountTreeDto>();

        foreach (var account in flatAccounts)
            if (account.ParentAccountId == null)
                roots.Add(account);
            else if (lookup.TryGetValue(account.ParentAccountId.Value, out var parent)) parent.Children.Add(account);

        return roots;
    }
}