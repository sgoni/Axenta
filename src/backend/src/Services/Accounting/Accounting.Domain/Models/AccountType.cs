namespace Accounting.Domain.Models;

/// <summary>
///     Catalog of account types
/// </summary>
public class AccountType : Entity<TypeId>
{
    private readonly List<Account> _accounts = new();
    public IReadOnlyCollection<Account> Accounts => _accounts;

    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public static AccountType Create(TypeId id, string name, string description)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);

        var accountType = new AccountType
        {
            Id = id,
            Name = name,
            Description = description
        };

        return accountType;
    }
}