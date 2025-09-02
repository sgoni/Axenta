namespace Accounting.Domain.Models;

/// <summary>
///     Catalog of account types
/// </summary>
public class AccountType : Entity<AccountTypeId>
{
    private readonly List<Account> _accounts = new();

    private AccountType()
    {
    } // EF

    public IReadOnlyCollection<Account> Accounts => _accounts.AsReadOnly();

    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public static AccountType Create(AccountTypeId id, string name, string? description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new AccountType
        {
            Id = id,
            Name = name,
            Description = description
        };
    }

    public void AddAccount(Account account)
    {
        ArgumentNullException.ThrowIfNull(account);
        _accounts.Add(account);
    }
}