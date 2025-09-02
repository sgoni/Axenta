namespace Accounting.Domain.Models;

/// <summary>
///     Account plan
/// </summary>
public class Account : Entity<AccountId>
{
    private readonly List<Account> _children = new();

    private Account()
    {
    } // Necesario para EF

    public IReadOnlyCollection<Account> Children => _children.AsReadOnly();

    public string Code { get; private set; } = default!;
    public string Name { get; private set; } = default!;
    public bool IsActive { get; private set; }
    public int Level { get; private set; }
    public bool IsMovable { get; private set; }

    public AccountTypeId AccountTypeId { get; private set; } = default!;
    public AccountType Type { get; private set; } = null!;

    public AccountId? ParentId { get; private set; }
    public Account? Parent { get; private set; }

    public static Account Create(AccountId id, string code, string name, AccountTypeId accountTypeId,
        AccountId? parentId, int level, bool isMovable)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        return new Account
        {
            Id = id,
            Code = code,
            Name = name,
            AccountTypeId = accountTypeId,
            ParentId = parentId,
            IsActive = true,
            Level = level,
            IsMovable = isMovable
        };
    }

    public void Update(string code, string name, AccountTypeId accountTypeId, AccountId? parentId, bool isActive)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Code = code;
        Name = name;
        AccountTypeId = accountTypeId;
        ParentId = parentId;
        IsActive = isActive;
    }

    public void AddChild(Account child)
    {
        ArgumentNullException.ThrowIfNull(child);
        _children.Add(child);
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