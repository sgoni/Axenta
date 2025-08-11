namespace Accounting.Domain.VelueObjects;

public record CompanyId
{
    private CompanyId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static CompanyId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("CompanyId cannot be empty");

        return new CompanyId(value);
    }
}