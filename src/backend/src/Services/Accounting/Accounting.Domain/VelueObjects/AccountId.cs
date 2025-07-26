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

    public static AccountId Of(Guid? parentAccountId)
    {
        //if (parentAccountId is null)
        //    throw new DomainException("ParentAccountId is required");

        //if (parentAccountId == Guid.Empty)
        //    throw new DomainException("ParentAccountId cannot be empty");

        if (!parentAccountId.HasValue || parentAccountId == Guid.Empty) return null;

        return new AccountId(parentAccountId.Value);
    }
}