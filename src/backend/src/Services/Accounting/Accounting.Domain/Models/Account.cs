namespace Accounting.Domain.Models;

/// <summary>
///     Account plan
/// </summary>
public class Account : Entity<AccountId>
{
    private readonly List<Account> _children = new();
    public IReadOnlyCollection<Account> Children => _children.AsReadOnly();

    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public AccountTypeId AccountTypeId { get; private set; } = default!;
    public AccountId? ParentId { get; private set; }
    public bool IsActive { get; private set; }
    public AccountType Type { get; private set; } = null!;
    public Account? Parent { get; private set; } = default!;

    public static Account Create(AccountId id, string code, string name, AccountTypeId accountTypeId,
        AccountId parentId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        var account = new Account
        {
            Id = id,
            Code = code,
            Name = name,
            AccountTypeId = accountTypeId,
            ParentId = parentId,
            IsActive = true
        };

        return account;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }
}