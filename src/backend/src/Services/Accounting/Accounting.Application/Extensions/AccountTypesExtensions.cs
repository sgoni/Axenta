namespace Accounting.Application.Extensions;

public static class AccountTypesExtensions
{
    public static IEnumerable<AccountTypeDto> ToAccountTypesDtoList(this IEnumerable<AccountType> accountTypes)
    {
        return accountTypes.Select(accountTypes => new AccountTypeDto(
            accountTypes.Id.Value,
            accountTypes.Name,
            accountTypes.Description
        ));
    }
}