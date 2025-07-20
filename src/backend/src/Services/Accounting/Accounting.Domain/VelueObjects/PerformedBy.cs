namespace Accounting.Domain.VelueObjects;

public record PerformedBy
{
    private PerformedBy(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static PerformedBy Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty) throw new DomainException("PerformedBy cannot be empty");

        return new PerformedBy(value);
    }
}