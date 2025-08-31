namespace Accounting.Domain.VelueObjects;

public record CompanyId : GuidValueObject
{
    public CompanyId(Guid value) : base(value)
    {
    }

    public static CompanyId Of(Guid value)
    {
        return new CompanyId(value);
    }
}