namespace Accounting.Domain.VelueObjects;

public record AccountTypeId
{
    private AccountTypeId(Guid value) => Value = value;
    public Guid Value { get; }

    public static AccountTypeId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("AccountTypeId cannot be empty");

        return new AccountTypeId(value);
    }
}