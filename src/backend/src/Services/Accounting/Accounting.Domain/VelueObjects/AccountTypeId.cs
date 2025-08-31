namespace Accounting.Domain.VelueObjects;

public record AccountTypeId : GuidValueObject
{
    public AccountTypeId(Guid value) : base(value)
    {
    }

    public static AccountTypeId Of(Guid value)
    {
        return new AccountTypeId(value);
    }
}