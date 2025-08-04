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
            account.ParentId?.Value,
            account.IsActive,
            account.Level,
            account.IsMovable
        ));
    }

    public static AccountDto DtoFromAccount(this Account account)
    {
        return new AccountDto(
            account.Id.Value,
            account.Code,
            account.Name,
            account.AccountTypeId.Value,
            account.ParentId?.Value,
            account.IsActive,
            account.Level,
            account.IsMovable);
    }

    public static List<AccountTreeDto>? ToAccountTreeDtoList(this IEnumerable<Account> accounts)
    {
        return new List<AccountTreeDto>(accounts.Select(account => new AccountTreeDto(
            account.Id.Value,
            account.Code,
            account.Name,
            account.AccountTypeId.Value,
            account.ParentId?.Value,
            new List<AccountTreeDto>()
        )));
    }

    public static List<AccountTreeDto> BuildTree(List<AccountTreeDto>? flatAccounts)
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