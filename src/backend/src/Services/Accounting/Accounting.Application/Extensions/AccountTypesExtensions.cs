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

    public static AccountTypeDto ToAccountTypeDto(this AccountType accountType)
    {
        return DtoFromAccountType(accountType);
    }

    public static AccountTypeDto DtoFromAccountType(AccountType accountType)
    {
        return new AccountTypeDto(
            accountType.Id.Value,
            accountType.Name,
            accountType.Description
        );
    }
}