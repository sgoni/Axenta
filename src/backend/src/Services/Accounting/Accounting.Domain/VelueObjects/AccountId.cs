namespace Accounting.Domain.VelueObjects;

public record AccountId
{
    private AccountId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static AccountId Of(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("AccountId cannot be empty");

        return new AccountId(value);
    }

    public static AccountId? FromNullable(Guid? value)
    {
        if (!value.HasValue || value == Guid.Empty) return null;
        return new AccountId(value.Value);
    }
}