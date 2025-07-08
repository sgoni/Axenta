namespace Accounting.Domain.VelueObjects;

public record PeriodId
{
    private PeriodId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PeriodId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("PeriodId cannot be empty");

        return new PeriodId(value);
    }
}