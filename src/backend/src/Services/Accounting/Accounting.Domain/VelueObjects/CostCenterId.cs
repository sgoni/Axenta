namespace Accounting.Domain.VelueObjects;

public record CostCenterId
{
    private CostCenterId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static CostCenterId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("CostCenterId cannot be empty");

        return new CostCenterId(value);
    }
}