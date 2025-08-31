namespace Accounting.Domain.VelueObjects;

public record AccountId : GuidValueObject
{
    public AccountId(Guid value) : base(value)
    {
    }

    public static AccountId Of(Guid value)
    {
        return new AccountId(value);
    }
    
    public static AccountId? FromNullable(Guid? parentAccountId)
    {
        if (!parentAccountId.HasValue || parentAccountId == Guid.Empty) return null;
        return new AccountId(parentAccountId.Value);
    }
}